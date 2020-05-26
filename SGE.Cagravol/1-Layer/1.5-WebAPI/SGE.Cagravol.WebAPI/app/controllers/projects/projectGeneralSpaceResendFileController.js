(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('ProjectGeneralSpaceResendFileController', ['$scope', '$routeParams', '$log', 'AppService', 'NetService', 'FlagService', 'EntityService', 'FileService', 'UtilService',
        function ($scope, $routeParams, $log, app, net, flag, entity, file, util) {

            app.setScopeSettings($scope, ['gspace', 'project']);
            
            $scope.id = 0;
            if (!isNaN($routeParams.id))
            {
                $scope.id = parseInt($routeParams.id || 0);
            }

            $scope.pid = 0;
            if (!isNaN($routeParams.pid)) {
                $scope.pid = $routeParams.pid || 0;
            }

            app.setState('project.gspace.resendfile', { id: $scope.id, pid: $scope.pid });

            var local = {
                progress: 0.0,
                channelId: '',
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

                    file.reSendFile({
                        channelId: local.channelId,
                        projectId: $scope.pid,
                        file: $scope.file,
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

                    var data = {
                        channelId: local.channelId
                    };
                    net.post.file.cancel(data);
                    
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
                    var f = e.target.files[0];
                    if (f.name !== $scope.item.fileName || f.type !== $scope.item.mimeType) {                        
                        local.removeSelectedFiles();
                        $scope.errorMessage = $scope.resx.errors.onResendFileChangeNameTypeDontMatch;                        
                    } else {
                        $scope.file = e.target.files[0];
                    }
                    
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
                setFileProperties: function () {

                    $scope.selectedFileType = $scope.types[0];
                    for (var x = 0; x < $scope.types.length ; x++) {
                        if ($scope.types[x].id === $scope.item.fileTypeId) {
                            $scope.selectedFileType = $scope.types[x];
                            break;
                        }
                    }
                    
                    local.channelId = $scope.item.channelId;

                },
                getFileSuccess: function (response) {
                    
                    if (response.data.success === true) {
                        if (response.data.message.trim() !== '') {
                            $scope.message = response.data.message.trim();
                        }
                        $log.log(response.data.value.file);
                        $scope.item = response.data.value.file;
                        local.setFileProperties();
                    } else {
                        $scope.errorMessage = response.data.errorMessage.trim();                        
                    }
                },
                getFileFail: function (error) {
                    if (error && error.message) {
                        $scope.errorMessage = error.message;
                    }
                },
                getFile: function () {
                    var data = { id: $scope.id, projectId: $scope.pid }, 
                        p = net.get.file.item(data);

                    p.then(local.getFileSuccess, local.getFileFail);
                },
                init: function () {
                    $scope.types = app.getFileTypeList();

                    app.endLoadingData();

                    local.getFile();

                    var backurl = app.getTemp(flag.TEMP.BACKTO_URL);
                    if (backurl) {
                        $scope.backurl = backurl;
                    }

                    var element = document.getElementById('item.url');
                    element.addEventListener('change', local.onFileChanged);
                }
            };

            $scope.fileContent = '';
            $scope.item = new entity.File();

            $scope.types = [];
            $scope.files = [];
            $scope.fileHashes = [];
            $scope.file = { size: 0 };
            $scope.selectedFileType = {};
            $scope.title = $scope.resx.resendFileTitle;
            $scope.description = $scope.resx.resendFileDescription;
            $scope.fileSelected = null;
            $scope.remainingTime = '';
            $scope.miniSpinnerIsVisible = false;
            $scope.backurl = '#/myspace/activity';

            $scope.doSendFile = local.doSendFile;
            $scope.preValidate = local.preValidate;
            $scope.onFileChanged = local.onFileChanged;
            $scope.getHumanSize = local.getHumanSize;
            $scope.removeSelectedFiles = local.removeSelectedFiles;
            $scope.getFileProgress = local.getFileProgress;
            $scope.getFileContent = local.getFileContent;

            local.init();
        }
    ]);

}(window.angular));