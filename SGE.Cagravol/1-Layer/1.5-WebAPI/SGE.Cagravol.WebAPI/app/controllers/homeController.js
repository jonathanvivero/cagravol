(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('HomeController', ['$scope','$http','AppService',
        function ($scope, $http, app) {

            $scope.resx = app.getI18n('home');

            app.endLoadingData();

        }
    ]);

}(window.angular));