(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('BodyController', ['$scope', '$http', 'AppService',
        function ($scope, $http, app) {
            $scope.resx = app.getI18n('common');
            $scope.showBody = function () {
                return !app.isLoadingData();
            };
        }
    ]);

}(window.angular));