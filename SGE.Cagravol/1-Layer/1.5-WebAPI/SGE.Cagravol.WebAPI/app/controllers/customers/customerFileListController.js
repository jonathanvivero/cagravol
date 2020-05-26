(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('CustomerFileListController', ['$scope', '$http', 'AppService', 'NetService',
        function ($scope, $http, app, net) {

            var local = {
                success: function (response) {
                    if (response.data.success) {
                        $scope.list = response.data.value.fileList;
                    } else {
                        alert(response.data.errorMessage);
                    }
                },
                fail: function (error) {
                    alert(error.data.message);
                },
                getListOfFiles: function () {

                    var data = {
                        userName: app.getCurrentUserName(),
                        projectId: 0
                    }

                    var p = net.post.project.files(data);

                    p.then(
                        local.success,
                        local.fail);

                },
                init: function () {
                    local.getListOfFiles();
                    app.endLoadingData();
                }
            };

            $scope.resx = app.getI18n('customerFileList');
            $scope.list = [];

            local.init();
        }
    ]);

}(window.angular));