(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('SignUpReservationController', ['$scope', '$timeout', '$routeParams', '$sce', 'AuthService', 'AppService', 'NetService', 'FlagService',
        function ($scope, $timeout, $routeParams, $sce, auth, app, net, flag) {

            app.setScopeSettings($scope, ['signUp', 'login']);

            $scope.cid = 0;
            if (!isNaN($routeParams.cid)) {
                $scope.cid = parseInt($routeParams.cid);
            }

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
                        $scope.registration.password.trim() === '' ||
                        $scope.registration.confirmPassword.trim() === '')
                        return;

                    var p = auth.saveRegistration($scope.registration, true);

                    p.then(local.success, local.fail);
                },
                spaceInfoSuccess: function (response) {
                    if (response.data.success === true) {
                        $scope.item = response.data.value.item;
                        $scope.registration.projectCode = $scope.item.signUpCode;
                        $scope.registration.userName = $scope.item.email;
                        $scope.registration.name = $scope.item.name;
                        $scope.enabledButton = true;

                    } else {
                        $scope.errorMessage = response.data.errorMessage;
                    }
                },
                spaceInfoFail: function (error) {
                    $scope.errorMessage = 'No es posible proceder con el registro del espacio reservado. Por favor, contacte con el administrador del sitio.';
                },
                init: function () {

                    var p = net.get.signup.spaceInfo($scope.cid);


                    p.then(local.spaceInfoSuccess, local.spaceInfoFail);

                    app.endLoadingData();
                }
            };

            $scope.savedSuccessfully = false;
            $scope.enabledButton = false;
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