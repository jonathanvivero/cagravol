(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('MySpaceActivityController', ['$scope', '$routeParams', 'AppService', 'NetService', 'FlagService', 'EntityService',
        function ($scope, $routeParams, app, net, flag, entity) {

            app.setScopeSettings($scope, ['myspace']);

            app.setState('myspace.activity');

            var showAlertAtSending = false,
                local = {
                success: function (response) {
                    if (response.data.success === true) {
                        if (response.data.message.trim() !== '') {
                            $scope.message = response.data.message.trim();
                        }
                        $scope.list = response.data.value.list;
                        $scope.projectId = response.data.value.projectId;
                        $scope.hasRecharge = response.data.value.hasRecharge;
                        $scope.isOutOfDate = response.data.value.isOutOfDate;
                        $scope.alertMessage = response.data.value.alertMessage;
                        $scope.willHaveRecharge = response.data.value.willHaveRecharge;

                        showAlertAtSending = $scope.hasRecharge || $scope.isOutOfDate || $scope.willHaveRecharge;

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
                    var p = net.get.myspace.list();

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
                    app.go.file.history({ id: item.id, pid: 0 });
                }, 
                resend: function (itemScope) {
                    if (showAlertAtSending === true) {
                        if ($scope.isOutOfDate || $scope.willHaveRecharge) {
                            alert($scope.alertMessage);
                        } else if ($scope.hasRecharge) {
                            var resp = confirm($scope.alertMessage);
                            if (resp === false) {
                                return;
                            }
                        }
                    }

                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.EDIT_ITEM, item);
                    app.go.myspace.resendfile(item.id);                
                },
                delete: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.DELETE_ITEM, item);
                    app.go.myspace.delete(item.id);
                },                
                new: function () {
                    if (showAlertAtSending === true) {
                        if ($scope.isOutOfDate || $scope.willHaveRecharge) {
                            alert($scope.alertMessage);
                        } else if ($scope.hasRecharge) {
                            var resp = confirm($scope.alertMessage);
                            if (resp === false) {
                                return;
                            }
                        }
                    }

                    app.go.myspace.newfile($scope.projectId);
                },
                clearFilterText: function () {
                    $scope.filterText = '';
                },
                select:function(item, index){
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
                goNew: function () {                                       
                    local.new();
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
            $scope.projectId = 0;
            $scope.title = $scope.resx.mainTitle;
            $scope.description = $scope.resx.mainDescription;
            $scope.select = local.select;
            $scope.selectedId = 0;
            $scope.selectedIndex = -1;

            $scope.clearFilterText = local.clearFilterText;

            $scope.goDelete = local.goDelete;
            $scope.goEdit = local.goEdit;
            $scope.goDownload = local.goDownload;
            $scope.goHistory = local.goHistory;
            $scope.goResend = local.goResend;
            $scope.goNew = local.goNew;

            local.init();
        }
    ]);

}(window.angular));