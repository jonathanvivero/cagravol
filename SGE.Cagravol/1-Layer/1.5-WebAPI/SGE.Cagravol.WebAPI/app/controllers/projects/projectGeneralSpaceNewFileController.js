(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('ProjectGeneralSpaceNewFileController', ['$scope', '$routeParams', '$log', 'AppService', 'NetService', 'FlagService', 'EntityService', 'FileService', 'UtilService',
        function ($scope, $routeParams, $log, app, net, flag, entity, file, util) {

            app.setScopeSettings($scope, ['gspace', 'project']);
            $scope.pid = 0;
            if (!isNaN($routeParams.pid)) {
                $scope.pid = parseInt($routeParams.pid || 0);
            }

            app.setState('project.gspace.newfile', $scope.pid);

            var local = {                
                progress: 0.0,
                channelId:'',
                getFileProgress: function () {
                    return local.progress;                   
                },                
                onStart: function (data) {
                    local.channelId = data.channelId;
                },
                onError: function (data) {
                    $scope.errorMessage = data.message;
                    local.resetRemainingTime();
                    net.post.file.cancel(data);
                },
                onProgress: function (data) {
                    local.progress = data.uploadedBytes; 
                    
                    var now = new Date(),
                        hours,
                        mins, 
                        secs = Math.abs((now - data.eft) / 1000);

                    var d = new Date(2016, 0, 1, 0, 0, 0);
                    d.setSeconds(secs);
                    secs = util.pad(d.getSeconds(), 2);
                    mins = util.pad(d.getMinutes(), 2);
                    hours = util.pad(d.getHours(), 2);

                    //$log.log(' Hora Prevista => ' + data.et);
                    //$log.log(' => ' + d);
                    //$log.log(' => ' + d);
                    //$log.log(' => ' + d);


                    var label = $scope.resx.remainingSeconds.replace('%h', hours);
                    label = label.replace('%m', mins);
                    label = label.replace('%s', secs);

                    $scope.remainingTime = label;                                        
                },
                onSuccess: function (data) {
                    app.setTemp(flag.TEMP.MYSPACE_ACTIVITY_MESSAGE, $scope.resx.fileUploadSuccess + '<a href="' + data.url + '" target="_blank">' + data.fileName + '</a>');
                    app.go.project.gspace.list($scope.pid);
                },
                onFinalizing: function (data) {
                    $scope.remainingTime = data.message;
                    $scope.miniSpinnerIsVisible = true;
                },
                doSendFile: function () {

                    local.resetRemainingTime();

                    $scope.remainingTime = $scope.resx.calculatingRemainingTime;

                    file.sendFile({
                        channelId: local.channelId,
                        projectId: $scope.pid,
                        file: $scope.file,
                        fileTypeId: $scope.selectedFileType.id,
                        item: $scope.item,
                        onSuccess: local.onSuccess,
                        onError: local.onError,
                        onFinalizing: local.onFinalizing,
                        onProgress: local.onProgress
                    });
                },
                removeSelectedFiles: function () {
                    $scope.fileContent = '';
                    $scope.file = new entity.File();
                    $scope.files = [];

                    //if (fileReader.channelId !== '') {
                        var data = {
                            channelId: local.channelId
                        };
                        net.post.file.cancel(data);
                    //}

                    var element = document.getElementById('item.url');
                    element.value = "";
                },
                preValidate: function () {
                    var result = true;

                    result = (result && ($scope.item.name.trim() !== ''));

                    result = (result && ($scope.file.size > 0));

                    result = (result && !app.isUploadingFile());

                    return result;
                },
                onFileChanged: function (e) {
                    $scope.errorMessage = '';
                    $scope.file = e.target.files[0];
                    $scope.$apply();
                },
                test: function () {
                    console.log($scope.fileSelected);
                },                
                setRemainingTime: function () {

                    if (fileReader.index % 12 === 0) {
                        local.recalculateEstimation();
                    }

                    var rest = Math.round((fileReader.estimatedFinalTime - new Date()) / 1000);

                    var d = new Date(2016, 0, 1, 0, 0, 0);
                    d.setSeconds(rest);
                    var secs = local.pad(d.getSeconds(), 2);
                    var mins = local.pad(d.getMinutes(), 2);
                    var hours = local.pad(d.getHours(), 2);

                    var label = $scope.resx.remainingTime.replace('%h', hours);
                    label = label.replace('%m', mins);
                    label = label.replace('%s', secs);

                    $scope.remainingTime = label;
                },
                resetRemainingTime: function () {
                    $scope.remainingTime = '';
                },
                recalculateEstimation: function () {
                    var parts = fileReader.parts,
                        index = fileReader.index,
                        now = new Date();

                    var eta = Math.abs(Math.round((now - fileReader.startTime) / 1000));
                    $log.log(eta);
                    eta = (eta / index);
                    $log.log(eta);
                    eta = (eta * parts);
                    $log.log(eta);

                    fileReader.estimatedFinalTime = new Date(fileReader.startTime);
                    fileReader.estimatedFinalTime.setSeconds(eta);
                    $log.log(fileReader.estimatedFinalTime);

                },
                init: function () {
                    var types = app.getFileTypeList();
                    $scope.types = types;

                    //for (var x; x < types.length ; x++) {
                    //    if (types[x].id === $scope.type.fileTypeId) {
                    //        $scope.selectedFileType = types[x];
                    //    }
                    //}                  

                    $scope.selectedFileType = types[0];
                    app.endLoadingData();
                    
                    var element = document.getElementById('item.url');
                    element.addEventListener('change', local.onFileChanged);
                    local.channelId = '';
                }
            };

            $scope.fileContent = '';
            $scope.item = new entity.File();
            $scope.isGSpace = true;
            $scope.types = [];
            $scope.files = [];
            $scope.fileHashes = [];
            $scope.file = { size: 0 };
            $scope.selectedFileType = {};
            $scope.title = $scope.resx.newfileTitle;
            $scope.description = $scope.resx.newFileDescription;
            $scope.fileSelected = null;
            $scope.doSendFile = local.doSendFile;
            $scope.preValidate = local.preValidate;
            $scope.test = local.test;
            $scope.onFileChanged = local.onFileChanged;
            $scope.getHumanSize = local.getHumanSize;
            $scope.removeSelectedFiles = local.removeSelectedFiles;
            $scope.getFileProgress = local.getFileProgress;
            $scope.getFileContent = local.getFileContent;
            $scope.remainingTime = '';
            $scope.miniSpinnerIsVisible = false;

            local.init();
        }
    ]);

}(window.angular));