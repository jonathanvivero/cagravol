(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('PanelController', ['$window', '$scope', 'AppService', 'NetService', 'EntityService', 'FlagService',
        function (root, $scope, app, net, entity, flag) {

            app.setScopeSettings($scope, ['panel']);
            app.setState('panel');

            var local = {
                success: function (response) {
                    root.console.info(response);

                    if (response.data.success === true) {
                        if (response.data.value.panelInfo) {
                            app.setUserPanels(response.data.value.panelInfo);
                            app.configNewRoutes(response.data.value.routes);

                            local.loadUserPanels(response.data.value.panelInfo);
                        }
                    } else {
                        $scope.errorMessage = errorResponse.data.errorMessage;
                    }

                    app.endLoadingData();
                },
                fail: function (errorResponse) {
                    $scope.errorMessage = errorResponse.data.message;
                    app.endLoadingData();
                },
                showMessage: function () {
                    return ($scope.message.trim() !== '')
                },
                showErrorMessage: function () {
                    return ($scope.errorMessage.trim() !== '')
                },
                showInfoBar: function () {
                    return ($scope.errorMessage.trim() !== '' || $scope.message.trim() !== '')
                },
                getPanelForCurrentUser: function () {
                    var p, success, panelsInfo;

                    panelsInfo = app.getUserPanels();

                    if (panelsInfo) {
                        local.loadUserPanels(panelsInfo);
                    } else {
                        var data = {userName: app.getCurrentUserName()}
                        p = net.get.panels(data);

                        p.then(local.success, local.fail);
                    }
                },
                loadUserPanels: function (panelsInfo) {

                    if (panelsInfo.top) {
                        $scope.panels.top = new entity.PanelRow(true,
                            panelsInfo.top.left.title, panelsInfo.top.left.url,
                            panelsInfo.top.center.title, panelsInfo.top.center.url,
                            panelsInfo.top.right.title, panelsInfo.top.right.url
                            );
                        app.endLoadingData();

                    } else {
                        $scope.panels.top = new entity.PanelRow(false);
                    }

                    if (panelsInfo.middle) {
                        $scope.panels.middle = new entity.PanelRow(true,
                            panelsInfo.middle.left.title, panelsInfo.middle.left.url,
                            panelsInfo.middle.center.title, panelsInfo.middle.center.url,
                            panelsInfo.middle.right.title, panelsInfo.middle.right.url
                            );
                        app.endLoadingData();

                    } else {
                        $scope.panels.middle = new entity.PanelRow(false);
                    }

                    if (panelsInfo.bottom) {
                        $scope.panels.bottom = new entity.PanelRow(true,
                            panelsInfo.bottom.left.title, panelsInfo.bottom.left.url,
                            panelsInfo.bottom.center.title, panelsInfo.bottom.center.url,
                            panelsInfo.bottom.right.title, panelsInfo.bottom.right.url
                            );
                        app.endLoadingData();

                    } else {
                        $scope.panels.bottom = new entity.PanelRow(false);
                    }

                },
                init: function () {

                    local.getPanelForCurrentUser();
                    app.endLoadingData();

                }
               
            };

            
            $scope.title = '';
            $scope.item = { title: '' };
            
            $scope.panels = {
                top: new entity.PanelRow(true),
                middle: new entity.PanelRow(false),
                bottom: new entity.PanelRow(false),
            }

            local.init();
        }
    ]);

}(window.angular));