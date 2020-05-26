(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('LoginController', ['$rootScope', '$routeParams', '$scope', 'AuthService', 'AppService', 'FlagService',
        function ($rootScope, $routeParams, $scope, auth, app, flag) {

            app.setScopeSettings($scope, ['login']);
            
            var local = {
                logInSuccess: function (response) {
                    $rootScope.$broadcast('auth-on');

                    if (response.roleFlag.trim() === 'C') {
                        app.go.myspace.activity();
                    } else {
                        app.go.panel();
                    }
                },
                logInFail: function (err) {
                    if (err) {
                        if (err.error_description) {                            
                            $scope.message = err.error_description;
                        } else {
                            $scope.message = $scope.resx.notPossibleToLogIn;
                        }
                    } else {
                        $scope.message = $scope.resx.notPossibleToLogIn;
                    }
                },
                logIn: function () {
                    var p = auth.login($scope.loginData);

                    p.then(local.logInSuccess, local.logInFail);
                },
                init: function () {
                    var tmpUserName = app.getTemp(flag.TEMP.JUST_SIGNED_UP) || $routeParams.email || '';
                    

                    $scope.loginData = {
                        userName: tmpUserName,
                        password: ''
                    };

                    app.endLoadingData();
                }
            };

            
            $rootScope.$broadcast('auth-off');

            $scope.loginData = {
                userName: '',
                password: ''
            };
            $scope.message = "";

            $scope.login = local.logIn;

            local.init();

        }]);
}(window.angular));