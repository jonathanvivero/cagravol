(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('MySpaceProfileController', ['$scope', '$routeParams', 'AppService', 'NetService', 'FlagService', 'EntityService',
        function ($scope, $routeParams, app, net, flag, entity) {

            app.setScopeSettings($scope, ['myspace']);

            app.setState('myspace.profile');

            var local = {
                success: function (response) {
                    if (response.data.success === true) {
                        if (response.data.message.trim() !== '') {
                            $scope.message = response.data.message.trim();
                        }
                        $scope.list = response.data.value.list;

                    } else {
                        $scope.errorMessage = response.data.errorMessage.trim();
                        if (response.data.errorCode.trim() === flag.ERROR_CODES.VALIDATION_ERROR) {

                        }
                    }
                },
                fail: function (error) {
                    $scope.errorMessage = error.errorMessage;
                },
                getList: function () {                    
                    var p = net.get.project.list();

                    p.then(local.success, local.fail);
                },
                edit: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.EDIT_ITEM, item);
                    app.go.myspace.edit(item.id);                
                }, 
                delete: function (itemScope) {
                    var item = itemScope.item;
                    app.setTemp(flag.TEMP.DELETE_ITEM, item);
                    app.go.myspace.delete(item.id);
                },
                new: function (item) {                    
                    app.go.myspace.new();
                },
                init: function () {
                    $scope.ctxMenu = [
                        [$scope.resx.common.edit, local.edit, true],
                        [$scope.resx.common.delete, local.delete, true],
                        [$scope.resx.resend, local.new, true],
                    ];

                    var msg = app.getTemp(flag.TEMP.MYSPACE_ACTIVITY_MESSAGE);
                    if (msg) {
                        $scope.message = msg;
                    }

                    local.getList();

                    app.endLoadingData();
                }
            };

            $scope.filterText = '';
            $scope.list = [];
            $scope.title = $scope.resx.mainTitle;
            $scope.description = $scope.resx.mainDescription;          

            local.init();
        }
    ]);

}(window.angular));