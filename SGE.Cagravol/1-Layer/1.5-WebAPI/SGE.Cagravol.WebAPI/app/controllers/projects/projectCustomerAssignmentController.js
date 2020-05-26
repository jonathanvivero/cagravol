(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('ProjectCustomerAssignmentController', ['$scope', '$routeParams', 'AppService', 'NetService', 'FlagService', 'EntityService',
        function ($scope, $routeParams, app, net, flag, entity) {

            app.setScopeSettings($scope, ['project','myspace']);
            app.setState('project.customer.assignment');

            $scope.id = 0;
            if (!isNaN($routeParams.id)) {
                $scope.id = parseInt($routeParams.id || 0);
            }

            var local = {
                
                init: function () {
                    
                    app.endLoadingData();
                }
            };
            
            local.init();
        }
    ]);

}(window.angular));