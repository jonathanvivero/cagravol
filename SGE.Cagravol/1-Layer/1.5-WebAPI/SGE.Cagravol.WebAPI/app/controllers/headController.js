(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('HeadController', ['$scope', '$http', 'AppService',
        function ($scope, $http, app) {
            $scope.resx = app.getI18n('common');
        }
    ]);

}(window.angular));