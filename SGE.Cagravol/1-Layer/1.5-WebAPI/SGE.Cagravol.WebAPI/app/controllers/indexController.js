(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('IndexController', ['$window', '$scope', '$http', '$location', '$rootScope', 'AppService', 'AuthService', 'NetService', 'FlagService',
        function (root, $scope, $http, $location, $rootScope, app, auth, net, flag) {

            var local = {
                onAuthOn: function () {
                    if ($scope.authentication.roleFlag.indexOf("M") >= 0) {
                        $scope.menuInc = '/app/views/partials/menu/man.html';
                    } else if ($scope.authentication.roleFlag.indexOf("S") >= 0) {
                        $scope.menuInc = '/app/views/partials/menu/man.html';
                    } else if ($scope.authentication.roleFlag.indexOf("O") >= 0) {
                        $scope.menuInc = '/app/views/partials/menu/man.html';
                    } else if ($scope.authentication.roleFlag.indexOf("C") >= 0) {
                        $scope.menuInc = '/app/views/partials/menu/cuz.html';
                    } else {
                        $scope.menuInc = 'empty.html';
                    }
                },
                onAuthOff: function () {
                    $scope.authentication.isAuth = false;
                    $scope.menuInc = 'empty.html';
                },
                success: function (response) {
                    if (response.data.success === true) {
                        app.setDefaultPlatformParameters(response.data.value);
                        app.raisePromisesResolution(flag.BROADCAST_SOURCES.DEFAULT_PARAMS, response.data.value);
                    } else {
                        root.console.warn($scope.resx.errors.onDefaultPlatformRequest);
                        root.console.warn(response);
                    }
                },
                fail: function (error) {
                    if (error.status === 401) {
                        local.onAuthOff();
                        $scope.logOut();
                    }
                    root.console.warn($scope.resx.errors.onDefaultPlatformRequest);
                    root.console.warn(error);
                },
                logOut: function () {
                    auth.logOut();
                    $location.path('/home');
                },
                showIndex: function () {
                    return !app.isLoadingData();
                },
                init: function () {
                    var p = net.get.defaultPlatformParameters();

                    p.then(local.success, local.fail);
                }
            };

            $scope.logOut = local.logOut;
            $scope.navbarExpanded = false;
            $scope.resx = app.getI18n('common');
            $scope.resx.errors = app.getI18n('errors');
            $scope.menus = app.getI18n('menus');
            $scope.menuInc = 'empty.html'
            $scope.authentication = app.getAuthData();
            $scope.showIndex = local.showIndex;
            $scope.$on('auth-on', local.onAuthOn);
            $scope.$on('auth-off', local.onAuthOff);


            if ($scope.authentication.isAuth === true) {
                local.onAuthOn();
            } else {
                local.onAuthOff();
            }

            local.init();

        }
    ]);

}(window.angular));