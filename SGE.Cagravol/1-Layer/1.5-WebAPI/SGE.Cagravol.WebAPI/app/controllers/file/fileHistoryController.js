(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('FileHistoryController', ['$window', '$log', '$routeParams', '$scope', '$http', '$location', '$rootScope', 'AppService', 'AuthService', 'NetService', 'FlagService', 'EntityService',
        function (root, $log, $routeParams, $scope, $http, $location, $rootScope, app, auth, net, flag, entity) {


            $scope.id = 0;
            if (!isNaN($routeParams.id)) {
                $scope.id = parseInt($routeParams.id || 0);
            }

            //Only in case of GSpace Files
            $scope.pid = 0;
            if (!isNaN($routeParams.pid)) {
                $scope.pid = parseInt($routeParams.pid || 0);
            }

            app.setScopeSettings($scope, ['file', 'myspace']);

            app.setState('file.history', { id: $scope.id, pid: $scope.pid });


            var auth = app.getAuthData(),
                local = {
                    success: function (response) {
                        if (response.data.success === true) {
                            $scope.item = response.data.value;
                            root.console.log($scope.item.file);
                            for (var i = 0; i < $scope.item.states.length; i++) {
                                $scope.commentCounter += $scope.item.states[i].notes.length;
                                $scope.newComment.push('');
                            }
                            local.setWFMovements();
                        } else {
                            $scope.errorMessage = response.data.errorMessage;
                        }
                    },
                    fail: function (error) {

                        if (error && error.data && error.data.message) {
                            $scope.errorMessage = error.data.errorMessage;
                        } else {
                            $scope.errorMessage = $scope.resx.common.onUnknowError;
                        }
                    },
                    getFile: function () {

                        var data = { id: $scope.id },
                            p = net.get.file.history(data);

                        p.then(local.success, local.fail);
                    },
                    setWFMovements: function () {                        
                        if ($scope.isC === true) {
                            $scope.userHaveRightsToMoveWF = app.isAbleWFMovement($scope.item.file.wfCurrentState.code);
                            $scope.cuzHasToResendFile = $scope.userHaveRightsToMoveWF;
                            app.setTemp(flag.TEMP.FILE, $scope.item.file);

                        } else {
                            var wfMoves;

                            if ($scope.pid === 0) {
                                wfMoves = app.getAbleWFMovements($scope.item.file.wfCurrentState.code);
                            } else {
                                wfMoves = app.getAbleWFGSMovements($scope.item.file.wfCurrentState.code);
                            }

                                if (wfMoves.length === 0) {
                                    $scope.wfIsAbleToMoveAhead = false;
                                    $scope.wfIsAbleToMoveBack = false;
                                } else {

                                    if (wfMoves[0].trim() === '') {
                                        $scope.wfIsAbleToMoveBack = false;
                                    } else {
                                        $scope.wfIsAbleToMoveBack = true;
                                        $scope.wfMoveBackTitle = $scope.resx.wfStates[wfMoves[0]];
                                        $scope.wfMoveBackCode = wfMoves[0];
                                    }

                                    if (wfMoves[1].trim() === '') {
                                        $scope.wfIsAbleToMoveAhead = false;
                                    } else {
                                        $scope.wfIsAbleToMoveAhead = true;
                                        $scope.wfMoveAheadTitle = $scope.resx.wfStates[wfMoves[1]];
                                        $scope.wfMoveAheadCode = wfMoves[1];
                                    }
                                }

                            $scope.userHaveRightsToMoveWF = true;
                        }
                    },
                    addCommentTo: function (state) {
                        $scope.addCommentToStateId = state.id;
                    },
                    commentIsOpen: function (state) {
                        return ($scope.addCommentToStateId === state.id);
                    },
                    addCommentSuccess: function (response) {
                        if (response.data.success === true) {
                            var item = response.data.value;
                            for (var i = 0; i < $scope.item.states.length; i++) {
                                if ($scope.item.states[i].id === item.stateId) {
                                    for (var x = 0; x < $scope.item.states[i].notes.length; x++) {
                                        if ($scope.item.states[i].notes[x].id === item.temporalId) {
                                            $scope.item.states[i].notes[x].id = item.newId;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    },
                    addCommentFail: function (error) {

                    },
                    saveComment: function (index) {
                        if ($scope.newComment[index].trim() !== '') {
                            $scope.commentCounter++;
                            var data = {
                                projectId: $scope.pid,
                                stateId: $scope.addCommentToStateId,
                                fileId: $scope.id,
                                id: (-1) * $scope.commentCounter,
                                comment: $scope.newComment[index],
                                ts: new Date(),
                                user: {
                                    email: auth.userName,
                                    id: '',
                                    name: '',
                                    surname: '',
                                    userName: auth.userName
                                }
                            };

                            var p = net.post.file.addCommentToState(data);

                            p.then(local.addCommentSuccess, local.addCommentFail);

                            for (var i = 0; i < $scope.item.states.length; i++) {
                                if ($scope.item.states[i].id === $scope.addCommentToStateId) {
                                    $scope.item.states[i].notes.push(data);
                                    break;
                                }
                            }
                            $scope.addCommentToStateId = 0;
                            $scope.newComment[index] = '';
                        }
                    },
                    cancelComment: function (index) {
                        $scope.addCommentToStateId = 0;
                        $scope.newComment[index] = '';
                    },
                    wfMovementSuccess: function (response) {
                        if (response.data.success === true) {
                            local.init();
                            $scope.message = $scope.resx.wfStateChangedSuccessfully_format.replace('{0}', response.data.value.wfCurrentState.state.name);
                        } else {
                            $scope.errorMessage = response.data.errorMessage;
                        }
                    },
                    wfMovementFail: function (error) {
                        if (error && error.message) {
                            $scope.errorMessage = error.message;
                        } else {
                            $scope.errorMessage = $scope.resx.errors.onWFStateChangeFailed;
                        }
                    },
                    confirmWFStateMovement:function(data){
                        var p = net.post.WF.move(data);
                        p.then(local.wfMovementSuccess, local.wfMovementFail);
                    },
                    moveWFBack: function () {
                        var comment = $scope.commentForWFStateChange || '';

                        if (comment.length < 5) {
                            alert($scope.resx.pleaseAddACommentForStateChange);
                            return;
                        }

                        $log.log($scope.wfMoveBackCode);
                        var data = {
                            projectId: $scope.pid,
                            fileId: $scope.id, 
                            version: $scope.item.file.version,
                            comment: comment,
                            movementCode: $scope.wfMoveBackCode,
                            wfCurrentStateId: $scope.item.file.wfCurrentState.id,
                            customerId: $scope.item.file.customerId
                        };
                        local.confirmWFStateMovement(data);
                    },
                    moveWFAhead:function(){
                        var comment = $scope.commentForWFStateChange || '';

                        //if (comment.length < 5) {
                        //    alert($scope.resx.pleaseAddACommentForStateChange);
                        //    return;
                        //}

                        $log.log($scope.wfMoveAheadCode);
                        var data = {
                            projectId: $scope.pid,
                            fileId: $scope.id,
                            version: $scope.item.file.version,
                            comment: comment,
                            movementCode: $scope.wfMoveAheadCode,
                            wfCurrentStateId: $scope.item.file.wfCurrentState.id,
                            customerId: $scope.item.file.customerId
                        };
                        local.confirmWFStateMovement(data);
                    },
                    init: function () {

                        $scope.isC = app.userIs.c();

                        if ($scope.id === 0) {
                            $scope.errorMessage = $scope.resx.errors.onFileNotExist;
                        } else {
                            local.getFile();
                        }

                        var btu = app.getTemp(flag.TEMP.BACKTO_URL);
                        if (btu){
                                    $scope.backToUrl = btu;
                        }
                    }
                };

            $scope.item = new entity.FileHistory();
            $scope.title = $scope.resx.historyTitle;
            $scope.description = $scope.resx.historyDescription;

            $scope.addCommentToStateId = 0;
            $scope.newComment = [];
            $scope.commentCounter = 0;
            $scope.userHaveRightsToMoveWF = false;
            $scope.cuzHasToResendFile = false;
            $scope.wfIsAbleToMoveAhead = false;
            $scope.wfIsAbleToMoveBack = false;
            $scope.wfMoveAheadTitle = '';
            $scope.wfMoveBackTitle = '';
            $scope.wfMoveAheadCode = '';
            $scope.wfMoveBackCode = '';
            $scope.isC = true;
            $scope.commentForWFStateChange = '';

            $scope.commentIsOpen = local.commentIsOpen;
            $scope.addCommentTo = local.addCommentTo;
            $scope.saveComment = local.saveComment;
            $scope.cancelComment = local.cancelComment;
            $scope.moveWFBack = local.moveWFBack;
            $scope.moveWFAhead = local.moveWFAhead;
            $scope.backToUrl = '#/myspace';
            local.init();

        }
    ]);

}(window.angular));