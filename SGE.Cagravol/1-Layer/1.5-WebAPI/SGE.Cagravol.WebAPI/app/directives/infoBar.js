
(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.directive('infoBar', ['$http', 'StoreFactory',
        function ($http, store) {
        return {
            restrict: 'E',
            replace: true,
            template: "<div class=\"row\" ng-show=\"showInfoBar()\"> \
                <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\" ng-show=\"showError()\"> \
                    <div data-ng-show=\"showError()\" class=\"alert alert-danger alert-dismissable\" role=\"alert\"> \
                        <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"{{resx.common.close}}\" ng-click=\"closeError()\"><span aria-hidden=\"true\">&times;</span></button> \
                        <div ng-bind-html=\"getErrorMessage()\"></div> \
                    </div> \
                </div> \
                <div class=\"col-lg-12 col-md-12 col-sm-12 col-xs-12\" ng-show=\"showMessage()\"> \
                    <div data-ng-show=\"showMessage()\" class=\"alert alert-success alert-dismissable\" role=\"alert\"> \
                        <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"{{resx.common.close}}\" ng-click=\"closeMessage()\"><span aria-hidden=\"true\">&times;</span></button> \
                        <div ng-bind-html=\"getGeneralMessage()\"></div> \
                    </div> \
                </div> \
            </div>",
            scope: false            
        };
    }]);
}(window.angular));


