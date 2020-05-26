(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('EditProjectController', ['$scope', '$window', '$routeParams', 'AppService', 'NetService', 'EntityService', 'FlagService', 'ValidationService',
        function ($scope, root, $routeParams, app, net, entity, flag, validation) {

            app.setScopeSettings($scope, ['project']);



            var local = {
                success: function (response) {
                    if (response.data.success === true) {
                        $scope.item = new entity.Project(response.data.value.item);
                    } else {
                        $scope.errorMessage = response.data.errorMessage;
                    }
                },
                fail: function (error) {
                    $scope.errorMessage = error.errorMessage;
                },
                getProject: function () {
                    var item = app.getTemp(flag.TEMP.EDIT_ITEM) || { id: -1},
                        data = {
                        id: $scope.id
                    };

                    if (item.id <= 0) {
                        return net.get.project.item(data);
                    } else {
                        $scope.item = item;
                        $scope.item.finishDate = new Date(item.finishDate);
                        $scope.item.startDate = new Date(item.startDate);
                        $scope.item.extraChargeForSendingDate = new Date(item.extraChargeForSendingDate);
                        $scope.item.limitForSendingDate = new Date(item.limitForSendingDate);

                        return undefined;
                    }                    
                },
                showNotExistMessage: function () {
                    return !$scope.itExist;
                },
                saveSuccess: function (response) {
                    if (validation.evaluateApiValidation($scope, response.data, entity.Project)) {
                        if ($scope.id === 0) {
                            app.setTemp(flag.TEMP.PROJECT_LIST_MESSAGE, $scope.resx.createdSuccess)
                        } else {
                            app.setTemp(flag.TEMP.PROJECT_LIST_MESSAGE, $scope.resx.modifiedSuccess)
                        }

                        //put the new id as the most recent project
                        app.setRecentProjectId($scope.item.id);
                        app.go.project.list();
                    }
                },
                saveFail: function (error) {
                    root.console.error(error);
                },
                preValidate: function () {
                    var result = true;

                    if ($scope.item.extraChargePercentage.$error !== null && $scope.item.extraChargePercentage.$error !== undefined) {
                        result = result && !$scope.item.extraChargePercentage.$error.float
                    }

                    return result;
                },
                doSave: function () {

                    if (!local.preValidate())
                        return;

                    var p,
                        data = $scope.item;

                    p = net.post.project.item(data);

                    p.then(local.saveSuccess, local.saveFail);
                },
                setDefaultTotalStands: function (e, data) {
                    $scope.item.totalStands = data.totalStands;
                },
                init: function () {
                    app.setDatePickerSettings($scope);

                    var defaultParams = app.getPlatformParams();

                    if ($scope.id === 0) {
                        app.setState('project.create');
                        if (!defaultParams) {
                            app.addPromiseResolution(flag.BROADCAST_SOURCES.DEFAULT_PARAMS, flag.BROADCAST_KEYS.SET_DEFAULT_PLATFORM_PARAMS);
                        } else {
                            //Default quantity for Stands;
                            $scope.item.totalStands = defaultParams.totalStands;
                        }

                        $scope.title = $scope.resx.createTitle;
                        $scope.description = $scope.resx.createDescription;
                        $scope.saveButtonLabel = $scope.resx.doCreate;
                    } else {
                        app.setState('project.edit', $scope.id);

                        $scope.title = $scope.resx.editTitle;
                        $scope.description = $scope.resx.editDescription;
                        $scope.saveButtonLabel = $scope.resx.doSave;

                        var p = local.getProject();

                        if (p) {
                            p.then(local.success, local.fail);
                        }

                        app.endLoadingData();
                    }
                }
            };

            $scope.$on(flag.BROADCAST_KEYS.SET_DEFAULT_PLATFORM_PARAMS, local.setDefaultTotalStands);

            $scope.popupStartDate = { opened: false };
            $scope.popupFinishDate = { opened: false };
            $scope.popupExtraChargeForSendingDate = { opened: false };
            $scope.popupLimitForSendingDate = { opened: false };

            $scope.openStartDate = function () {
                $scope.popupStartDate.opened = true;
            };
            $scope.openFinishDate = function () {
                $scope.popupFinishDate.opened = true;
            };
            $scope.openExtraChargeForSendingDate = function () {
                $scope.popupExtraChargeForSendingDate.opened = true;
            };
            $scope.openLimitForSendingDate = function () {
                $scope.popupLimitForSendingDate.opened = true;
            };
            $scope.preValidate = local.preValidate;
            
            $scope.id = $routeParams.id || 0;
            $scope.itExist = true;
            $scope.title = '';
            $scope.description = '';
            $scope.saveButtonLabel = '';
            $scope.item = new entity.Project();

            $scope.dtOptions = {
                dateDisabled: false,
                showWeeks: true
            };


            $scope.showNotExistMessage = local.showNotExistMessage;
            $scope.doSave = local.doSave;

            local.init();
        }
    ]);

}(window.angular));