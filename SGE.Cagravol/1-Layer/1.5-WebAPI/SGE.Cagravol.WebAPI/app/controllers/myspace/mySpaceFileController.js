﻿(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('MySpaceFileController', ['$scope', '$routeParams', 'AppService', 'NetService', 'FlagService', 'EntityService',
        function ($scope, $routeParams, app, net, flag, entity) {

            app.setScopeSettings($scope, ['myspace']);

            $scope.id = 0;
            if (!isNaN($routeParams.id)) {
                $scope.id = parseInt($routeParams.id || 0);
            }
            app.setState('myspace.file', $scope.id);

            var local = {
                updateSuccess: function (response) {
                    if (response.data.success === true) {
                        app.setTemp(flag.TEMP.MESSAGE_ACTIVITY_MESSAGE, $scope.resx.modifiedSuccesfully),
                        app.go.myspace.activity($scope.pid);
                    } else {
                        $scope.errorMessage = response.data.errorMessage;
                    }
                },
                updateFail: function (error) {
                    $scope.errorMessage = error.data.errorMessage;
                },
                doUpdate: function () {

                    if (!local.preValidate())
                        return;

                    $scope.item.fileType = $scope.selectedFileType;
                    $scope.item.fileTypeId = $scope.selectedFileType.id;                    

                    var p = net.post.myspace.edit($scope.item);

                    p.then(local.updateSuccess, local.updateFail);
                },
                getFileSuccess: function (response) {
                    if (response.data.success === true) {
                        $scope.item = response.data.value.item;

                        //$scope.selectedFileType = types[0];
                        for (var x = 0; x < $scope.types.length ; x++) {
                            if ($scope.types[x].id === $scope.item.fileTypeId) {
                                $scope.selectedFileType = $scope.types[x];
                            }
                        }
                    } else {
                        $scope.errorMessage = response.data.errorMessage;
                    }
                },
                getFileFail: function (error) {
                    $scope.errorMessage = error.data.errorMessage;
                },
                getFile: function () {

                    var p = null,
                        item = app.getTemp(flag.TEMP.EDIT_ITEM) || { id: 0 };

                    if (item.id === $scope.id) {
                        $scope.item = item;                        
                        for (var x = 0; x < $scope.types.length ; x++) {
                            if ($scope.types[x].id === $scope.item.fileTypeId) {
                                $scope.selectedFileType = $scope.types[x];
                            }
                        }
                    } else {
                        var data = { id: $scope.id, projectId: $scope.pid };
                        p = net.get.myspace.item(data);
                    }

                    if (p) {
                        p.then(local.getFileSuccess, local.getFileFail);
                    }
                },
                preValidate: function () {
                    var result = true;

                    result = (result && ($scope.item.name.trim() !== ''));

                    return result;
                },
                init: function () {                    
                    local.getFile();
                    app.endLoadingData();
                }
            };

            $scope.item = new entity.File();
            $scope.selectedFileType = {};
            $scope.types = app.getFileTypeList();
            $scope.backUrl = '#/myspace/activity';
            $scope.doUpdate = local.doUpdate;
            $scope.preValidate = local.preValidate;

            local.init();
        }
    ]);

}(window.angular));