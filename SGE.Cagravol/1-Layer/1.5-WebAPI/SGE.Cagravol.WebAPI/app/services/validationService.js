(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.service('ValidationService', [ 'FlagService', 
        function (flag) {
            var self = this,                
                local = {                    
                    evaluateApiValidation: function ($scope, responseData, newItemCb) {
                        newItemCb = newItemCb || function () { };
                        if (responseData.success === true) {                            
                            $scope.item = new newItemCb(responseData.value.item);
                            return true;
                        } else if (responseData.errorCode === flag.ERROR_CODES.VALIDATION_ERROR) {
                            $scope.errorMessage = responseData.errorMessage;
                            return false;
                        } else {
                            $scope.errorMessage = responseData.errorMessage;
                            return false;
                        }
                    }
                };

            self.evaluateApiValidation = local.evaluateApiValidation;            
            
            return self;
        }
    ]);

}(window.angular));
