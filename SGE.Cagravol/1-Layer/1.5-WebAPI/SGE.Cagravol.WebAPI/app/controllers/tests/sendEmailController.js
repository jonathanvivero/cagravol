(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('TestSendEmailController', ['$scope','NetService','AppService', '$log',
        function ($scope, net, app, $log) {

            app.setScopeSettings($scope, ['test']);
            app.setState('test.sendemail');            

            var local = {
                success:function(response){
                    $log.info(response);
                },
                fail: function (error) {
                    $log.error(error);
                },
                sendEmail:function(){                
                    var data = {
                        email: $scope.email,
                        message: $scope.mailMessage
                    };

                    var p = net.get.test.sendEmail(data);
                    p.then(local.success, local.fail);
                    
                },
                init: function () {
                    app.endLoadingData();
                }
            };

            $scope.email = 'jonathanvivero@gmail.com';
            $scope.mailMessage = 'Hola desde Gafidec';

            $scope.sendEmail = local.sendEmail;
            local.init();
        }
    ]);

}(window.angular));