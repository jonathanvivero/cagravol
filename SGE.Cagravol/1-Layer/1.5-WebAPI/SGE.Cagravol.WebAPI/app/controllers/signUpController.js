(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('SignUpController', ['$scope', '$timeout', '$sce', 'AuthService', 'AppService', 'NetService', 'FlagService',
        function ($scope, $timeout, $sce, auth, app, net, flag) {

            var local = {
                success: function (response) {

                    $scope.savedSuccessfully = response.data.success;
                    if (response.data.success === true) {
                        $scope.message = $scope.resx.registrationSuccess;

                        app.setTemp(flag.TEMP.JUST_SIGNED_UP, $scope.registration.userName);
                        app.startTimer(app.getDefault.milisecondsForRedirection, app.go.login);
                    } else {
                        $scope.message = response.data.errorMessage;
                    }
                    app.endLoadingData();
                },
                fail: function (errorResponse) {    
                    var errors = [];
                    
                    for (var key in errorResponse.data.modelState) {
                        for (var i = 0; i < errorResponse.data.modelState[key].length; i++) {
                            errors.push(errorResponse.data.modelState[key][i]);
                        }
                    }
                    $scope.savedSuccessfully = false;
                    $scope.message = $scope.resx.registrationFailedBy + errors.join(' ');
                    app.endLoadingData();
                },
                signUp: function () {

                    if ($scope.registration.projectCode.trim() === '' || 
                        $scope.registration.userName.trim() === '' ||
                        $scope.registration.name.trim() === '' ||
                        $scope.registration.password.trim() === '' ||
                        $scope.registration.confirmPassword.trim() === '')
                        return;                    
                    
                    var p = auth.saveRegistration($scope.registration);

                    p.then(local.success, local.fail);
                },
                init: function () {

                    //Temp for testing
                    

                    app.endLoadingData();
                }

            };
            
            $scope.resx = app.getI18n('signUp');
            $scope.resx.login = app.getI18n('login');
            $scope.savedSuccessfully = false;
            $scope.message = "";
            $scope.registration = {
                projectCode: "",
                userName: "",
                name: "",
                password: "",
                confirmPassword: ""
            };
            $scope.signUp = local.signUp;
            $scope.getInstructions = function (text) {
                return $sce.trustAsHtml(text);
            };

            local.init();

        }]);
}(window.angular));