(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('DeleteProjectController', ['$scope', '$routeParams', 'AppService', 'FlagService', 'EntityService', 'NetService', 
        function ($scope, $routeParams, app, flag, entity, net) {

            app.setScopeSettings($scope, ['project']);

            var local = {
                deleteSuccess: function (response) {
                    if (response.data.success === true) {
                        app.setTemp(flag.TEMP.PROJECT_LIST_MESSAGE, $scope.resx.deletedSuccess)
                        app.go.project.list();
                    } else {
                        $scope.errorMessage = response.data.errorMessage;
                    }
                },
                deleteFail: function (error) {
                    $scope.errorMessage = $scope.resx.errors.onDelete;
                },
                confirmDelete: function () {

                    var p = net.delete.project({ id: $scope.id });
                    p.then(local.deleteSuccess, local.deleteFail);
                },

                showDeleteButton: function () {
                    return $scope.isDeleteable;
                },
                loadSuccess: function (response) {
                    if (response.data.success === true) {
                        $scope.item = new entity.Project(response.data.value.item);
                        local.displayItem();
                    } else {
                        $scope.errorMessage = response.data.errorMessage;
                    }
                },
                loadFail: function (error) {
                    $scope.errorMessage = error.errorMessage;
                },
                getProject: function () {
                    var data = {
                        id: $scope.id
                    };

                    return net.get.project.item(data);
                },
                displayItem: function () {

                    $scope.resx.title = $scope.resx.deleteTitle;

                    if ($scope.id === $scope.item.id.toString()) {
                        $scope.resx.description = $scope.resx.deleteDescription;
                        $scope.isDeleteable = true;
                    } else {
                        $scope.resx.description = $scope.resx.deleteDescriptionDoesNotExistItem;
                        $scope.isDeleteable = false;
                    }
                },
                init: function () {
                    app.setState('project.delete', $scope.id);

                    var item = app.getTemp(flag.TEMP.DELETE_ITEM);

                    if (item) {
                        $scope.item = new entity.Project(item); 
                        local.displayItem();
                    } else {
                        var p = local.getProject();
                        p.then(local.loadSuccess, local.loadFail);
                    }

                    app.endLoadingData();
                }
            };


            $scope.item = new entity.Project();
            $scope.id = $routeParams.id || 0;
            $scope.isDeleteable = false;

            $scope.showDeleteButton = local.showDeleteButton;
            $scope.confirmDelete = local.confirmDelete;

            local.init();

        }
    ]);

}(window.angular));