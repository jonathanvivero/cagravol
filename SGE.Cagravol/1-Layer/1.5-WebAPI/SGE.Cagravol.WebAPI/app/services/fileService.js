(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.service('FileService', ['$log', 'AppService', 'NetService', 'FlagService', 'UtilService', 'EntityService',
        function ($log, app, net, flag, util, entity) {

            var self = this,                
                local = {
                    resx:{},
                    fileReader: null,
                    file: null,
                    fileTypeId: 0,
                    projectId: 0,
                    item:{},
                    fn: function (data) { },
                    onStart: function (data) { },
                    onError: function (data) { },
                    onProgress: function (data) { },
                    onSuccess: function (data) { },
                    onFinalizing: function (data) { },
                    setFile: function (file) {
                        local.file = file;
                    },
                    setItem: function (item) {
                        local.item = item;
                    },
                    reset: function () {
                        local.file = null;
                        local.item = {};
                        local.fileReader = new entity.FileReader();
                    },
                    sendFile: function (config) {                      

                        local.init(config);

                        if (local.fileReader.totalFileSize > 0 && local.fileReader.channelId !== '') {
                            local.fileReader.index = 1;

                            local.fileReader.startTime = new Date();
                            local.fileReader.estimatedFinalTime = null;

                            local.onStart({ channelId: local.fileReader.channelId });

                            local.sendNextHash();
                            return;
                        } else {
                            local.startFileUpload();
                        }
                    },
                    reSendFile: function (config) {                      

                        local.init(config);

                        if (local.fileReader.totalFileSize > 0 && local.fileReader.channelId !== '') {
                            local.fileReader.index = 1;

                            local.fileReader.startTime = new Date();
                            local.fileReader.estimatedFinalTime = null;

                            local.onStart({ channelId: local.fileReader.channelId });

                            //local.sendNextHash();
                            local.startFileUpload();
                            return;
                        } else {
                            config.message = local.resx.common.errors.onReSendFileNoDataFound;
                            local.onError(config);
                        }
                    },

                    startFileSuccess: function (response) {
                        var fur = null; // => fur = fileUploadResponse

                        if (response.data.success === true) {
                            fur = response.data.value;
                            local.fileReader.index = fur.index + 1;
                            local.fileReader.channelId = fur.channelId;

                            local.recalculateEstimation();

                            var eta = Math.round((local.fileReader.estimatedFinalTime - local.fileReader.startTime) / 1000, 0);
                            var uploadedBytes = ((local.fileReader.totalFileSize / local.fileReader.parts) * (local.fileReader.index - 1));
                            local.onProgress({
                                channelId: local.fileReader.channelId,
                                index: local.fileReader.index,
                                parts: local.fileReader.parts,
                                size: local.fileReader.totalFileSize,
                                eft: local.fileReader.estimatedFinalTime,
                                eta: eta,
                                uploadedBytes: uploadedBytes
                            });
                            local.sendNextHash();
                        } else {
                            $log.error(error);
                            local.fileReader.index = 0;
                            app.finishFileUpload();

                            local.onError({
                                channelId: local.fileReader.channelId,
                                message: local.resx.errors.onUploadErrorOrCancel,
                                on: 'startFile'
                            });
                            local.reset();
                        }
                    },
                    startFileFail: function (error) {
                        $log.error(error);
                        local.fileReader.index = 0;
                        app.finishFileUpload();
                        local.onError({
                            channelId: local.fileReader.channelId,
                            message: local.resx.errors.onUploadErrorOrCancel, on: 'startFile'
                        });
                        local.reset();
                    },
                    startFileUpload: function () {
                        var p,
                           data = null,
                           rest = 0,
                           parts = 0;

                        local.fileReader.index = 0;
                        local.fileReader.totalFileSize = local.file.size;

                        if (local.file.size <= local.fileReader.bufferSize) {
                            parts = 1;
                        } else {
                            rest = (local.file.size % local.fileReader.bufferSize);
                            parts = (local.file.size - rest) / local.fileReader.bufferSize;
                            if (rest > 0) {
                                parts += 1;
                            }
                        }

                        local.fileReader.parts = parts;

                        app.startFileUpload();

                        data = {
                            projectId: local.projectId,
                            channelId: local.fileReader.channelId,
                            size: local.file.size,
                            fileName: local.file.name,
                            fileNotes: local.item.fileNotes,
                            fileTypeId: local.fileTypeId,
                            mimeType: local.file.type,
                            parts: parts,
                            customerLogicalName: local.item.name,
                        }

                        local.fileReader.startTime = new Date();
                        local.fileReader.estimatedFinalTime = null;

                        p = net.post.file.init(data);

                        local.onStart({ channelId: local.fileReader.channelId });

                        p.then(local.startFileSuccess, local.startFileFail);
                    },
                    sendFileHashSuccess: function (response) {
                        var fur = null; // => fur = fileUploadResponse

                        if (response.data.success === true) {

                            local.recalculateEstimation();

                            fur = response.data.value;
                            local.fileReader.index = fur.index + 1;

                            var now = new Date();
                            var eta = Math.round((now - local.fileReader.estimatedFinalTime) / 1000, 0);
                            var uploadedBytes = ((local.fileReader.totalFileSize / local.fileReader.parts) * (local.fileReader.index - 1));
                            local.onProgress({
                                channelId: local.fileReader.channelId,
                                index: local.fileReader.index,
                                parts: local.fileReader.parts,
                                size: local.fileReader.totalFileSize,
                                eft: local.fileReader.estimatedFinalTime,
                                eta: eta,
                                uploadedBytes: uploadedBytes
                            });
                            local.sendNextHash();
                        } else {
                            //local.resetRemainingSeconds();
                            $log.error(response.data);
                            local.fileReader.index = 0;
                            app.finishFileUpload();

                            local.onError({
                                channelId: local.fileReader.channelId,
                                message: response.data.errorMessage, on: 'sendFileHash'
                            });
                            local.reset();
                        }
                    },
                    sendFileHashFail: function (error) {
                        var msg = error.data || { message: local.resx.errors.onUploadErrorOrCancel },
                            message = error.data.message || local.resx.errors.onUploadErrorOrCancel;

                        $log.error(error);
                        local.fileReader.index = 0;
                        app.finishFileUpload();
                        local.onError({
                            channelId: local.fileReader.channelId,
                            message: message, on: 'sendFileHash'
                        });
                        local.reset();
                    },
                    sendNextHash: function () {

                        try {
                            var blob = null,
                                start = local.fileReader.bufferSize * (local.fileReader.index - 1),
                                end = start + local.fileReader.bufferSize;                          

                            if (start > local.fileReader.totalFileSize) {
                                local.endFileUpload();
                                return;
                            }

                            if (end > local.fileReader.totalFileSize) {
                                end = local.fileReader.totalFileSize + 1;
                            }

                            if (local.file.webkitSlice) {
                                blob = local.file.webkitSlice(start, end);
                            } else if (local.file.mozSlice) {
                                blob = local.file.mozSlice(start, end);
                            } else {
                                blob = local.file.slice(start, end);
                            }

                            local.fileReader.reader.readAsArrayBuffer(blob);

                        } catch (e) {
                            local.fileReader.index = 0;
                            //local.resetRemainingSeconds();
                            local.onError({
                                channelId: local.fileReader.channelId,
                                message: local.resx.errors.onUserCancelUpload
                            });
                        }
                    },
                    readAsArrayBufferOnLoad: function (e) {
                        var p;

                        if (local.fileReader.channelId === '') {
                            local.onError({
                                channelId: local.fileReader.channelId,
                                message: local.resx.errors.onHashUploadChannelEmpty
                            });
                            return;
                        } else {
                            var hash = util.arrayBufferToBase64(e.target.result),
                                data = {
                                    hash: hash,
                                    channelId: local.fileReader.channelId,
                                    size: e.target.result.size,
                                    index: local.fileReader.index
                                };

                            p = net.post.file.hash(data);

                            p.then(local.sendFileHashSuccess, local.sendFileHashFail);
                        }
                    },
                    endFileSuccess: function (response) {
                        if (response.data.success) {
                            local.fileReader.index += 1;
                            local.onSuccess({
                                channelId: local.fileReader.channelId,
                                url: response.data.value.url, fileName: local.file.name
                            });
                        } else {
                            //local.resetRemainingSeconds();
                            $log.error(response.data);
                            local.fileReader.index = 0;
                            app.finishFileUpload();
                            local.onError({ message: response.data.errorMessage, on: 'endFile' });
                            local.reset();
                        }
                        app.finishFileUpload();
                    },
                    endFileFail: function (error) {
                        $log.error(response.data);
                        local.fileReader.index = 0;
                        app.finishFileUpload();
                        local.onError({
                            channelId: local.fileReader.channelId,
                            message: local.resx.errors.onUploadErrorOrCancel, on: 'endFile'
                        });
                        local.reset();
                    },
                    endFileUpload: function () {
                        var p,
                            data = {
                                channelId: local.fileReader.channelId,
                                fileId: local.item.id,
                                projectId: local.projectId,
                            };

                        local.onFinalizing({ message: local.resx.composingFileInDestination});
                        
                        p = net.post.file.end(data);

                        p.then(local.endFileSuccess, local.endFileFail);
                    },
                    recalculateEstimation: function () {
                        var parts = local.fileReader.parts,
                            index = local.fileReader.index,
                            now = new Date();

                        if (local.fileReader.estimatedFinalTime === null) {
                            var estimation = ((now - local.fileReader.startTime) / 1000);
                            //makes an estimation in seconds
                            estimation = Math.abs(estimation) * (local.fileReader.parts * 1.2);
                            //add seconds to the current date to set the estimated time of finishing                        
                            local.fileReader.estimatedFinalTime = new Date();
                            local.fileReader.estimatedFinalTime.setSeconds(local.fileReader.estimatedFinalTime.getSeconds() + estimation);
                        }

                        if (local.fileReader.index % 12 === 0 || local.fileReader.index === 1) {

                            //$log.log('------------------------------------------------------------------------------------------------------------------------------------------');
                            //$log.log('------------------------------------------------------------------------------------------------------------------------------------------');
                            //$log.log('START::' + local.fileReader.startTime);
                            //$log.log('NOW::::' + now);
                            //$log.log('index => ' + index);

                            var eta = (now - local.fileReader.startTime) / 1000;
                            //$log.log('(now - local.fileReader.startTime) / 1000 => ' + eta);
                            eta = (eta / index);
                            //$log.log('eta / index => ' + eta);
                            eta = (eta * parts);
                            //$log.log('eta * parts => ' + eta);

                            eta = Math.abs(eta);

                            local.fileReader.estimatedFinalTime = new Date(local.fileReader.startTime);
                            local.fileReader.estimatedFinalTime.setSeconds(eta);
                            $log.log('Hora Prevista Subida => ' + local.fileReader.estimatedFinalTime);
                        }
                    },
                    init: function (config) {
                        local.reset();

                        if (config) {
                            util.checkAndAssign.fn(config.onStart, local, 'onStart');
                            util.checkAndAssign.fn(config.onError, local, 'onError');
                            util.checkAndAssign.fn(config.onProgress, local, 'onProgress');
                            util.checkAndAssign.fn(config.onSuccess, local, 'onSuccess');
                            util.checkAndAssign.fn(config.onFinalizing, local, 'onFinalizing');

                            util.checkAndAssign.obj(config.item, local, 'item');
                            util.checkAndAssign.obj(config.file, local, 'file');
                            util.checkAndAssign.obj(config.fileTypeId, local, 'fileTypeId');
                            util.checkAndAssign.obj(config.projectId, local, 'projectId');
                            util.checkAndAssign.obj(config.channelId, local.fileReader, 'channelId');

                            if (local.file) {
                                local.fileReader.totalFileSize = local.file.size;
                            }
                        }                        
                        local.fileReader.reader.onload = local.readAsArrayBufferOnLoad;

                        local.resx = app.getI18n('services.file');
                        local.resx.common = app.getI18n('common');
                        local.resx.common.errors = app.getI18n('errors');
                    }
                };

            self.sendFile = local.sendFile;
            self.reSendFile = local.reSendFile;

            return self;
        }
    ]);

}(window.angular));
