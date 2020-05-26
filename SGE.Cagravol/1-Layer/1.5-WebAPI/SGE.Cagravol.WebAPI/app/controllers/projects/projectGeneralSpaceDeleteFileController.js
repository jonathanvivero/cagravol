(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('ProjectGeneralSpaceDeleteFileController', ['$scope', '$timeout', '$routeParams', 'AppService', 'NetService', 'FlagService', 'EntityService',
        function ($scope, $timeout, $routeParams, app, net, flag, entity) {

            app.setScopeSettings($scope, ['gspace', 'project']);

            $scope.id = 0;
            if (!isNaN($routeParams.id)) {
                $scope.id = $routeParams.id || 0;
            }

            $scope.pid = 0;
            if (!isNaN($routeParams.pid)) {
                $scope.pid = $routeParams.pid || 0;
            }

            app.setState('project.gspace.deletefile', { id: $scope.id, pid: $scope.pid });

            var local = {
                success: function (response) {
                    if (response.data.success) {
                        $scope.item = response.data.value.item;
                        local.displayItem();
                    } else {
                        $scope.errorMessage = response.data.errorMessage;
                    }
                },
                fail: function (error) {
                    $scope.errorMessage = error.errorMessage;
                },
                getItem: function () {

                    var data = {
                        id: $scope.id
                    };

                    var p = net.get.myspace.item(data);

                    p.then(local.success, local.fail);
                },
                displayItem: function () {
                    if ($scope.id === $scope.item.id.toString()) {
                        $scope.description = $scope.resx.deleteDescription;
                        $scope.isDeleteable = true;
                    } else {
                        $scope.description = $scope.resx.deleteDescriptionDoesNotExistItem;
                        $scope.isDeleteable = false;
                    }
                },
                deleteSuccess: function (response) {
                    if (response.data.success === true) {
                        app.setTemp(flag.TEMP.MYSPACE_ACTIVITY_MESSAGE, $scope.resx.deletedSuccess);
                        app.go.project.gspace.list($scope.pid);
                    } else {
                        $scope.errorMessage = response.data.errorMessage;
                    }
                },
                deleteFail: function (error) {
                    $scope.errorMessage = $scope.resx.errors.onDelete;
                },
                confirmDelete: function () {

                    var p = net.delete.gspace({ id: $scope.id });
                    p.then(local.deleteSuccess, local.deleteFail);
                },
                showDeleteButton: function () {
                    return $scope.isDeleteable;
                },
                init: function () {
                    var item = app.getTemp(flag.TEMP.DELETE_ITEM);
                    
                    if (item) {
                        $scope.item = entity.File(item);
                        local.displayItem();
                    } else {
                        local.getItem();
                    }

                    var backurl = app.getTemp(flag.TEMP.BACKTO_URL);

                    if (backurl) {
                        $scope.backurl = backurl;                        
                    } 
                    
                    app.endLoadingData();                    
                }
            };

            $scope.item = new entity.File();
            $scope.backurl = '#/myspace/activity';
            $scope.isDeleteable = false;

            $scope.showDeleteButton = local.showDeleteButton;
            $scope.confirmDelete = local.confirmDelete;


            local.init();
        }
    ]);

}(window.angular));