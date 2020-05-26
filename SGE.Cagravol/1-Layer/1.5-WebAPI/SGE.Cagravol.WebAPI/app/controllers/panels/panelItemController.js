(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('PanelItemController', ['$window', '$scope', 'AppService', 'NetService', 'EntityService', 'FlagService',
        function (root, $scope, app, net, entity, flag) {

            var local = {                
            };

            $scope.title = '';            
            $scope.resx = app.getI18n('panel');
           
        }
    ]);

}(window.angular));