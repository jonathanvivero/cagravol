(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('ProjectGeneralSpaceActivityController', ['$scope', '$routeParams', 'AppService', 'NetService', 'FlagService', 'EntityService',
        function ($scope, $routeParams, app, net, flag, entity) {

            app.setScopeSettings($scope, ['gspace', 'project']);

            $scope.pid = 0;
            if (!isNaN($routeParams.id))
            {
                $scope.pid = $routeParams.id || 0;
            }

            app.setState('project.gspace.activity', $scope.pid);

            var local = {
                success: function (response) {
                    if (response.data.success === true) {
                        if (response.data.message.trim() !== '') {
                            $scope.message = response.data.message.trim();
                        }
                        $scope.list = response.data.value.list;

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

                    var data = { id: 0, pId: $scope.pid },
                        p = net.get.project.gspace.list(data);

                    p.then(local.success, local.fail);
                },
                edit: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.EDIT_ITEM, item);
                    app.setTemp(flag.TEMP.BACKTO_URL, '#/project/gspace/activity/' + $scope.pid);
                    app.go.project.gspace.edit({ id: item.id, pid: $scope.pid });
                },
                history: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.EDIT_ITEM, item);
                    app.setTemp(flag.TEMP.BACKTO_URL, '#/project/gspace/activity/' + $scope.pid);
                    app.go.file.history({ id: item.id, pid: $scope.pid });
                },
                resend: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.EDIT_ITEM, item);
                    app.setTemp(flag.TEMP.BACKTO_URL, '#/project/gspace/activity/' + $scope.pid);
                    app.go.project.gspace.resendfile({ id: item.id, pid: $scope.pid });
                },
                delete: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.DELETE_ITEM, item);
                    app.setTemp(flag.TEMP.BACKTO_URL, '#/project/gspace/activity/' + $scope.pid);
                    app.go.project.gspace.delete({ id: item.id, pid: $scope.pid });
                },
                new: function (item) {
                    app.setTemp(flag.TEMP.BACKTO_URL, '#/project/gspace/activity/' + $scope.pid);
                    app.go.project.gspace.new($scope.pid);
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
                    var item = $scope.list[$scope.selectedIndex];
                    local.edit({ item: item });
                },
                goHistory: function () {
                    var item = $scope.list[$scope.selectedIndex];
                    local.history({ item: item });
                },
                goResend: function () {
                    var item = $scope.list[$scope.selectedIndex];
                    local.resend({ item: item });
                },
                goDelete: function () {
                    var item = $scope.list[$scope.selectedIndex];
                    local.delete({ item: item });
                },
                goDownload: function () {
                    var item = $scope.list[$scope.selectedIndex];
                    local.download({ item: item });
                },
                init: function () {
                    $scope.ctxMenu = [
                        [$scope.resx.new, local.new, true],
                        [$scope.resx.edit, local.edit, true],
                        [$scope.resx.history, local.history, true],
                        [$scope.resx.resend, local.resend, true],
                        [$scope.resx.common.delete, local.delete, true],
                    ];

                    var msg = app.getTemp(flag.TEMP.MYSPACE_ACTIVITY_MESSAGE);
                    if (msg) {
                        $scope.message = msg;
                    }

                    local.getList();

                    app.endLoadingData();
                }
            };

            $scope.filterText = '';
            $scope.list = [];
            $scope.title = $scope.resx.mainTitle;
            $scope.description = $scope.resx.mainDescription;
            $scope.selectedId = 0;
            $scope.selectedIndex = -1;

            $scope.clearFilterText = local.clearFilterText;

            $scope.select = local.select;
            $scope.goDelete = local.goDelete;
            $scope.goEdit = local.goEdit;
            $scope.goDownload = local.goDownload;
            $scope.goHistory = local.goHistory;
            $scope.goResend = local.goResend;

            local.init();
        }
    ]);

}(window.angular));