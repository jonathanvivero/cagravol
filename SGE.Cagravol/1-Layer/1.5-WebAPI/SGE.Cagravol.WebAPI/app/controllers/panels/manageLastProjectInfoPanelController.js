(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('ManageLastProjectInfoPanelController', ['$window', '$scope', 'AppService', 'NetService', 'EntityService', 'FlagService',
        function (root, $scope, app, net, entity, flag) {

            var local = {
                init: function () {
                    $scope.projectId = app.getRecentProjectId();
                }
            };

            $scope.projectId = 0;
            $scope.resx = app.getI18n('managerInfoPanels').manageLastProject;

            local.init();
           
        }
    ]);

}(window.angular));