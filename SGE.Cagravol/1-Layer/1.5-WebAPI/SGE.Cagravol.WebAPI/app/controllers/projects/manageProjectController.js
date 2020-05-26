(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('ManageProjectController', ['$scope', '$routeParams', 'AppService',
        function ($scope, $routeParams, app) {
            $scope.resx = app.getI18n('project');
            $scope.id = $routeParams.id || 0;

            if ($scope.id === 0) {
                $scope.resx.title = $scope.resx.createTitle;
                $scope.resx.description = $scope.resx.createDescription;
            } else {
                $scope.resx.title = $scope.resx.editTitle;
                $scope.resx.description = $scope.resx.editDescription;
            }

        }
    ]);

}(window.angular));