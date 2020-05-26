(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('ListProjectController', ['$scope', '$routeParams', 'AppService', 'NetService', 'FlagService', 'EntityService',
        function ($scope, $routeParams, app, net, flag, entity) {

            app.setScopeSettings($scope, ['project']);

            app.setState('project.list');

            var local = {
                success: function (response) {
                    if (response.data.success === true) {
                        if (response.data.message.trim() !== '') {
                            $scope.message = response.data.message.trim();
                        }
                        $scope.list = response.data.value.list;

                    } else {
                        $scope.errorMessage = response.data.errorMessage.trim();
                        if (response.data.errorCode)
                        {
                            if (response.data.errorCode.trim() === flag.ERROR_CODES.VALIDATION_ERROR) {
                            }
                        }
                    }
                },
                fail: function (error) {
                    $scope.errorMessage = $scope.resx.common.errors.onGettingList;
                    //if (error && error.message) {
                    //    $scope.errorMessage = error.message;
                    //} else {
                    //    $scope.errorMessage = $scope.resx.common.errors.onGettingList;
                    //}
                },
                getList: function () {                    
                    var p = net.get.project.list();

                    p.then(local.success, local.fail);
                },
                create: function () {
                    app.go.project.create();
                },
                edit: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.EDIT_ITEM, item);
                    app.go.project.edit(item.id);                
                }, 
                delete: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.DELETE_ITEM, item);
                    app.go.project.delete(item.id);
                },
                activity: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.ACTIVITY_ITEM, item);
                    app.go.project.activity(item.id);
                },
                clearFilterText: function () {
                    $scope.filterText = '';
                },
                downloadExcelSuccess: function (response) {
                    if (response.data.success === true) {
                        window.open(response.data.value.url, '_blank');
                    }else{
                        alert(response.data.errorMessage);
                    }
                },
                downloadExcelFail: function (error) {
                    alert("falló la solicitud de archivo excel");
                },
                downloadExcel: function (itemScope) {
                    var item = itemScope.item, 
                        data = {id: item.id}, 
                        p;
                    
                    p = net.post.project.excel(item);

                    p.then(local.downloadExcelSuccess, local.downloadExcelSuccess);                    
                },
                select: function (item, index) {
                    $scope.selectedId = item.id;
                    $scope.selectedIndex = index;
                    //$scope.selectedUrl = item.url;
                },
                showCreate: function () {
                    return (app.userIs.a_or_m());
                },

                showEdit: function () {
                    return ($scope.selectedIndex >= 0 && app.userIs.a_or_m());
                },
                showDelete: function () {
                    return ($scope.selectedIndex >= 0 && app.userIs.a_or_m());
                },
                showActivity: function () {
                    return ($scope.selectedIndex >= 0 && app.userIs.s());
                },
                showDownloadExcel: function () {
                    return ($scope.selectedIndex >= 0 && app.userIs.o());
                },
                goCreate: function () {
                    var item = $scope.list[$scope.selectedIndex];
                    local.create({ item: item });                    
                },
                goEdit: function () {
                    var item = $scope.list[$scope.selectedIndex];
                    local.edit({ item: item });                    
                },
                goDelete: function () {
                    var item = $scope.list[$scope.selectedIndex];
                    local.delete({ item: item });
                },
                goActivity: function () {
                    var item = $scope.list[$scope.selectedIndex];
                    local.activity({ item: item });
                },
                goDownloadExcel: function () {
                    var item = $scope.list[$scope.selectedIndex];
                    local.downloadExcel({ item: item });
                },
                init: function () {

                    $scope.ctxMenu = [
                        [$scope.resx.common.edit, local.edit, app.userIs.a_or_m()],
                        [$scope.resx.common.delete, local.delete, app.userIs.a_or_m()],
                        [$scope.resx.common.activity, local.activity, app.userIs.s()],
                        [$scope.resx.downloadExcel, local.downloadExcel, app.userIs.o()],
                    ];

                    var msg = app.getTemp(flag.TEMP.PROJECT_LIST_MESSAGE);
                    if (msg) {
                        $scope.message = msg;
                    }

                    local.getList();

                    app.endLoadingData();
                }

            };

            $scope.filterText = '';
            $scope.list = [];
            $scope.resx.title = $scope.resx.listTitle;
            $scope.resx.description = $scope.resx.listDescription;

            $scope.clearFilterText = local.clearFilterText;

            $scope.select = local.select;
            $scope.selectedId = 0;
            $scope.selectedIndex = -1;

            
            $scope.showCreate = local.showCreate;
            $scope.showEdit = local.showEdit;
            $scope.showDelete = local.showDelete;
            $scope.showActivity = local.showActivity;
            $scope.showDownloadExcel = local.showDownloadExcel;
            $scope.goCreate = local.goCreate;
            $scope.goEdit = local.goEdit;
            $scope.goDelete = local.goDelete;
            $scope.goActivity = local.goActivity;
            $scope.goDownloadExcel = local.goDownloadExcel;

            
            local.init();
        }
    ]);

}(window.angular));