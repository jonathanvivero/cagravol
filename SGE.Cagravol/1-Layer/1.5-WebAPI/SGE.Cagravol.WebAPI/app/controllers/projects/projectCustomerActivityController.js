(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('ProjectCustomerActivityController', ['$scope', '$window', '$routeParams', 'AppService', 'NetService', 'FlagService', 'EntityService',
        function ($scope, $window, $routeParams, app, net, flag, entity) {

            app.setScopeSettings($scope, ['project', 'myspace']);

            $scope.id = 0;
            $scope.pid = 0;

            if (!isNaN($routeParams.id)) {
                $scope.id = parseInt($routeParams.id || 0);
            }
            if (!isNaN($routeParams.pid)) {
                $scope.pid = parseInt($routeParams.pid || 0);
            }

            app.setState('project.customer.activity', {pid: $scope.pid, id: $scope.id});

            var local = {
                success: function (response) {
                    if (response.data.success === true) {
                        if (response.data.message.trim() !== '') {
                            $scope.message = response.data.message.trim();
                        }
                        $scope.customer = response.data.value.customer;
                    } else {
                        $scope.errorMessage = response.data.errorMessage.trim();
                        if (response.data.errorCode.trim() === flag.ERROR_CODES.VALIDATION_ERROR) {

                        }
                    }
                },
                fail: function (error) {
                    $scope.errorMessage = error.errorMessage;
                },
                getList: function () {
                    var data = {
                        id: $scope.id,
                        pId: $scope.pid
                    },
                        p = net.get.project.customerActivity(data);

                    p.then(local.success, local.fail);
                },
                edit: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.EDIT_ITEM, item);
                    app.go.myspace.edit(item.id);
                },
                history: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.EDIT_ITEM, item);
                    app.setTemp(flag.TEMP.BACKTO_URL, '#/project/customer/activity/' + $scope.pid + '/' + $scope.id);
                    app.go.file.history({id: item.id, pid: 0});
                },
                resend: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.EDIT_ITEM, item);
                    app.go.myspace.resendfile(item.id);
                },
                delete: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.DELETE_ITEM, item);
                    app.go.myspace.delete(item.id);
                },
                new: function (item) {
                    app.go.myspace.new();
                },
                download:function(itemScope){
                    var item = itemScope.item;
                    alert('Va a descargar ' + item.fileName);
                    $window.open(item.url, '_blank');
                    //app.setTemp(flag.TEMP.DELETE_ITEM, item);
                    //app.go.myspace.delete(item.id);
                },
                clearFilterText: function () {
                    $scope.filterText = '';
                },
                select: function (item, index) {
                    $scope.selectedId = item.id;
                    $scope.selectedIndex = index;
                    $scope.selectedUrl = item.url;
                },
                goEdit: function () {
                    var item = $scope.customer.files[$scope.selectedIndex];
                    local.edit({ item: item });
                },
                goHistory: function () {
                    var item = $scope.customer.files[$scope.selectedIndex];                    
                    local.history({ item: item });
                },
                goResend: function () {
                    var item = $scope.customer.files[$scope.selectedIndex];
                    local.resend({ item: item });
                },
                goDelete: function () {
                    var item = $scope.customer.files[$scope.selectedIndex];
                    local.delete({ item: item });
                },
                goDownload: function () {
                    var item = $scope.customer.files[$scope.selectedIndex];
                    local.download({ item: item });
                },
                init: function () {
                    $scope.ctxMenu = [
                        [$scope.resx.myspace.history, local.history, true],
                        [$scope.resx.common.download, local.download, true]
                    ];

                    var msg = app.getTemp(flag.TEMP.PROJECT_CUSTOMER_ACTIVITY_MESSAGE);
                    if (msg) {
                        $scope.message = msg;
                    }

                    local.getList();

                    app.endLoadingData();
                }
            };

            $scope.filterText = '';
            $scope.customer = {};
            $scope.title = $scope.resx.mainTitle;
            $scope.description = $scope.resx.mainDescription;
            $scope.selectedId = 0;
            $scope.selectedIndex = -1;

            $scope.clearFilterText = local.clearFilterText;

            $scope.select = local.select;
            $scope.goHistory = local.goHistory;
            $scope.goDownload = local.goDownload;

            local.init();
        }
    ]);

}(window.angular));