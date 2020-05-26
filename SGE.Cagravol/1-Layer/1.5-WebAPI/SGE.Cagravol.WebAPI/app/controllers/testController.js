(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('TestController', ['$window', '$scope', 'AppService', 'NetService',
        function (root, $scope, app, net) {

            var local = {
                testSuccessFn: function (response) {
                    root.console.info(response);
                    $scope.message = 'test Ok!';
                },
                testFailFn: function (errorResponse) {
                    $scope.message = errorResponse.data.message;
                },
                getTest: function () {
                    var p, success;

                    p = net.get.test.test();

                    p.then(local.testSuccessFn, local.testFailFn);
                    app.endLoadingData();
                }

            };

            $scope.message = '';
            $scope.resx = app.getI18n('test');

            local.getTest();

        }
    ]);

}(window.angular));