(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.controller('ProjectActivityController', ['$scope', '$routeParams', '$log', 'AppService', 'NetService', 'EntityService', 'FlagService', 'UtilService',
        function ($scope, $routeParams, $log, app, net, entity, flag, util) {

            app.setScopeSettings($scope, ['project', 'signUp']);
            app.setState('project.activity', $routeParams.id);

            $scope.id = $routeParams.id || 0;

            var local = {
                currentAssignmentIndex: -1,
                currentEliminationIndex: -1,
                currentReservationIndex: -1,
                currentResetIndex: -1,
                getItemSuccess: function (response) {
                    if (response.data.success === true) {
                        $scope.item = response.data.value.item;
                        $scope.generalSpace = {};

                        angular.copy($scope.generalSpace, $scope.item.customers[0]);

                        $scope.item.customers.shift();

                    } else {
                        $scope.errorMessage = response.data.errorMessage;
                    }

                },
                getItemFail: function (error) {
                    $scope.errorMessage = error.errorMessage;
                },
                getItem: function () {
                    var item = app.getTemp(flag.TEMP.ACTIVITY_ITEM) || { id: 0 };

                    if ($scope.id.toString() === item.id.toString()) {
                        $scope.item = item;
                        $scope.generalSpace = {};

                        angular.copy($scope.generalSpace, item.customers[0]);

                        item.customers.shift();

                    } else {
                        var p = net.get.project.item({ id: $scope.id });
                        p.then(local.getItemSuccess, local.getItemFail);
                    }
                },
                cuzType: function (index) {
                    /*
                    Type 0 => Free,
                    Type 1 => Reserved,
                    Type 2 => Registered,
                    */
                    var idx = index || 0,
                        type = 0;
                    if ($scope.item.customers[idx].registered === true) {
                        type = 2;
                    } else if ($scope.item.customers[idx].reserved === true) {
                        type = 1;
                    }

                    return type;
                },
                isCuzType: function (index, _type) {
                    return (local.cuzType(index) === __type);
                },
                emailExists: function (email) {
                    var res = (email !== null);
                    return res;
                },
                getGridClass: function () {
                    return 'col-lg-' + $scope.gridTypeList[$scope.gridType] + ' col-md-' + $scope.gridTypeList[$scope.gridType] + ' col-sm-' + $scope.gridTypeList[$scope.gridType] + ' col-xs-' + $scope.gridTypeList[$scope.gridType];
                },
                setListTypeTo: function (index) {
                    $scope.gridType = index;
                },
                assign: function (itemScope) {
                    var item = itemScope.customer;
                    console.log(item);
                },
                getPanelClass: function (index) {
                    var t = local.cuzType(index);

                    if (t === 0) {
                        return 'panel-default';
                    } else if (t === 1) {
                        return 'panel-warning';
                    }

                    return 'panel-primary';
                },
                getColorClass: function (index, prefix) {

                    if (index === $scope.eliminationIndex || index === $scope.resetIndex) {
                        return prefix + '-danger';
                    } else if (index === $scope.reservationIndex || index === $scope.assignmentIndex) {
                        return prefix + '-info';
                    } else {
                        var t = local.cuzType(index);
                        if (t === 0) {
                            return prefix + '-default';
                        } else if (t === 1) {
                            return prefix + '-warning';
                        }
                    }

                    return prefix + '-primary';
                },

                removeCustomerSpace: function (index) {
                    index = index || 0;
                    $scope.eliminationIndex = index;
                },
                reserveCustomerSpace: function (index) {
                    index = index || 0;
                    $scope.reservationIndex = index;
                },
                viewCustomerFiles: function (index) {
                    index = index || 0;

                    var cuz = $scope.item.customers[index];
                    app.go.project.customer.activity({ id: cuz.id, pid: $scope.id });
                },
                resetCustomerSpace: function (index) {
                    index = index || 0;
                    $scope.emailForReset[index] = $scope.item.customers[index].email;
                    $scope.resetIndex = index;
                },
                assignCustomerSpace: function (index) {
                    index = index || 0;

                    if ($scope.item.customers[index].reserved === true) {
                        $scope.emailForAssignment[index] = $scope.item.customers[index].email;
                    }

                    $scope.assignmentIndex = index;
                },
                assignmentSuccess: function (response) {
                    var resp = response.data.value;

                    if (response.data.success === true) {
                        $log.log('Assignment Ok for ' + resp.email);
                    } else if (response.data.success === false || resp === undefined || resp === null) {

                        $scope.errorMessage = response.data.errorMessage;

                        $scope.item.customers[local.currentAssignmentIndex].registered = $scope.tempSpaceAssign.registered;
                        $scope.item.customers[local.currentAssignmentIndex].reserved = $scope.tempSpaceAssign.reserved;
                        $scope.item.customers[local.currentAssignmentIndex].email = $scope.tempSpaceAssign.email;

                        //alert(response.data.errorMessage);
                    }

                    $scope.tempSpaceAssign = {};
                    local.currentAssignmentIndex = -1;
                }, assignmentFail: function (error) {
                    $scope.item.customers[local.currentAssignmentIndex].registered = true;
                    $scope.item.customers[local.currentAssignmentIndex].reserved = true;
                    $scope.item.customers[local.currentAssignmentIndex].email = 'PROBLEMA: Refrescar para ver...';

                    $scope.tempSpaceAssign = {};

                    $scope.errorMessage = "Error de Asignacion para el espacio " + $scope.item.customers[local.currentAssignmentIndex].signUpCode;

                    //alert("Error on Assigment: " + $scope.item.customers[local.currentAssignmentIndex].signUpCode);

                    local.currentAssignmentIndex = -1;
                },
                assignmentOk: function (index) {
                    index = index || 0;
                    
                    var em = $scope.emailForAssignment[index] || '',
                        pw = $scope.passwordForAssignment[index] || '', 
                        p;

                    if (!util.validateEmail(em)) {
                        alert(em + ' no es un Email válido');
                    } else if (!util.checkPasswordSecurity(pw)) {
                        alert($scope.resx.signUp.instructions_c);
                    } else {
                        var cStatus = 0;
                        if ($scope.item.customers[index].reserved === true) {
                            cStatus = 1;
                        }

                        $scope.emailForAssignment[index] = '';
                        $scope.passwordForAssignment[index] = '';
                        $scope.assignmentIndex = -1;                        

                        $scope.tempSpaceAssign.registered = $scope.item.customers[index].registered;
                        $scope.tempSpaceAssign.reserved = $scope.item.customers[index].reserved;
                        $scope.tempSpaceAssign.email = $scope.item.customers[index].email;

                        $scope.item.customers[index].registered = true;
                        $scope.item.customers[index].reserved = true;
                        $scope.item.customers[index].email = em;

                        var data = {
                            customerId: $scope.item.customers[index].id,
                            projectId: $scope.id,
                            email: em,
                            password: pw,
                            index: index,
                            currentStatus: cStatus,
                            newStatus: 2,
                        };

                        local.currentAssignmentIndex = index;

                        p = net.post.project.space(data);

                        p.then(local.assignmentSuccess, local.assignmentFail);
                    }
                        
                },
                assignmentCancel: function (index) {
                    index = index || 0;

                    $scope.assignmentIndex = -1;
                },
                reservationSuccess: function (response) {
                    var resp = response.data.value;

                    if (response.data.success === true) {
                        $log.log('Reservation Ok for ' + resp.email);
                    }
                    else {
                        $scope.item.customers[resp.index].registered = resp.registered;
                        $scope.item.customers[resp.index].reserved = resp.reserved;
                        $scope.item.customers[resp.index].email = resp.email;

                        alert(response.data.errorMessage);
                    }

                    local.currentReservationIndex = -1;
                },
                reservationFail: function (error) {
                    $scope.item.customers[local.currentReservationIndex].registered = true;
                    $scope.item.customers[local.currentReservationIndex].reserved = true;
                    $scope.item.customers[local.currentReservationIndex].email = 'PROBLEMA: Refrescar para ver...';

                    alert("Error on Reservation: " + $scope.item.customers[local.currentReservationIndex].signUpCode);

                    local.currentReservationIndex = -1;
                },
                reservationOk: function (index) {
                    index = index || 0;
                    var em = $scope.emailForReservation[index] || '';

                    if (!util.validateEmail(em)) {
                        alert(em + ' no es un Email válido');
                    } else {                        

                        for (var x = 0; x < $scope.item.customers.length ; x++) {
                            if (em === $scope.item.customers[x].email) {
                                alert('Ya existe un usuario con el mismo email a quien se le ha asignado un espacio. No es posible Reservar más de un espacio por usuario.');
                                return;
                            }
                        }
                        
                        $scope.emailForReservation[index] = '';

                        $scope.reservationIndex = -1;

                        $scope.item.customers[index].registered = false;
                        $scope.item.customers[index].reserved = true;
                        $scope.item.customers[index].email = em;

                        var p,
                            data = {
                                customerId: $scope.item.customers[index].id,
                                projectId: $scope.id,
                                email: em,
                                password: '',
                                index: index,
                                currentStatus: 0,
                                newStatus: 1,
                            };

                        local.currentAssignmentIndex = index;

                        p = net.post.project.space(data);

                        p.then(local.reservationSuccess, local.reservationFail);
                        
                    }
                    
                },
                reservationCancel: function (index) {
                    index = index || 0;

                    $scope.reservationIndex = -1;
                },
                eliminationSuccess: function (response) {
                    var resp = response.data.value;

                    if (response.data.success === true) {
                        $log.log('Elimination Ok for ' + resp.email);
                    } else {
                        $scope.item.customers[resp.index].registered = resp.registered;
                        $scope.item.customers[resp.index].reserved = resp.reserved;
                        $scope.item.customers[resp.index].email = resp.email;

                        alert(response.data.errorMessage);
                    }

                    local.currentEliminationIndex = -1;
                },
                eliminationFail: function (error) {
                    $scope.item.customers[local.currentReservationIndex].registered = true;
                    $scope.item.customers[local.currentReservationIndex].reserved = true;
                    $scope.item.customers[local.currentReservationIndex].email = 'PROBLEMA: Refrescar para ver...';

                    alert("Error on Elimination: " + $scope.item.customers[local.currentReservationIndex].signUpCode);

                    local.currentEliminationIndex = -1;
                },
                eliminationOk: function (index) {
                    index = index || 0;

                    $scope.item.customers[index].email                    
                    var p,
                        em = $scope.item.customers[index].email,
                        data = {
                            customerId: $scope.item.customers[index].id,
                            projectId: $scope.id,
                            email: em,
                            password: '',
                            index: index,
                            currentStatus: 2,
                            newStatus: 0,
                        };

                    $scope.eliminationIndex = -1;
                    $scope.item.customers[index].registered = false;
                    $scope.item.customers[index].reserved = false;
                    $scope.item.customers[index].email = '';

                    local.currentEliminationIndex = index;

                    p = net.post.project.space(data);

                    p.then(local.eliminationSuccess, local.eliminationFail);

                },
                eliminationCancel: function (index) {
                    index = index || 0;

                    $scope.eliminationIndex = -1;
                },
                resetSuccess: function (response) {
                    var resp = response.data.value;

                    if (response.data.success === true) {
                        $log.log('Reservation Ok for ' + resp.email);
                    } else {
                        $scope.item.customers[resp.index].registered = resp.registered;
                        $scope.item.customers[resp.index].reserved = resp.reserved;
                        $scope.item.customers[resp.index].email = resp.email;

                        alert(response.data.errorMessage);
                    }

                    local.currentResetIndex = -1;
                },
                resetFail: function (error) {
                    $scope.item.customers[local.currentReservationIndex].registered = false;
                    $scope.item.customers[local.currentReservationIndex].reserved = true;
                    $scope.item.customers[local.currentReservationIndex].email = 'PROBLEMA: Refrescar para ver...';

                    alert("Error on Elimination: " + $scope.item.customers[local.currentReservationIndex].signUpCode);

                    local.currentResetIndex = -1;
                },
                resetOk: function (index) {
                    index = index || 0;

                    var p,
                        em = $scope.item.customers[index].email,
                        data = {
                            customerId: $scope.item.customers[index].id,
                            projectId: $scope.id,
                            email: em,
                            password: '',
                            index: index,
                            currentStatus: 1,
                            newStatus: 0,
                        };

                    $scope.emailForReset[index] = '';
                    $scope.item.customers[index].email = '';
                    $scope.item.customers[index].reserved = false;
                    $scope.item.customers[index].registered = false;
                    $scope.resetIndex = -1;

                    local.currentResetIndex = index;

                    p = net.post.project.space(data);

                    p.then(local.eliminationSuccess, local.eliminationFail);
                },
                resetCancel: function (index) {
                    index = index || 0;

                    $scope.resetIndex = -1;
                },
                resetPassword: function (itemScope) {
                    var item = itemScope.item;
                },
                init: function () {
                    local.getItem();

                    if ($scope.id === 0) {
                        $scope.resx.title = $scope.resx.createTitle;
                        $scope.resx.description = $scope.resx.createDescription;
                    } else {
                        $scope.resx.title = $scope.resx.editTitle;
                        $scope.resx.description = $scope.resx.editDescription;
                    }
                    //test@test.com
                    $scope.ctxMenu = [
                        ['Asignar', local.assign, true],
                        ['Resetear Contrase&ntilde;a', local.resetPassword, true]
                    ];

                }
            };

            $scope.generalSpace = {};
            $scope.item = new entity.Project();
            $scope.title = $scope.resx.activityTitle;
            $scope.description = $scope.resx.activityDescription;
            $scope.emailExists = local.emailExists;
            $scope.getGridClass = local.getGridClass;
            $scope.spaceStatusList = $scope.resx.spaceStatusList;
            $scope.setListTypeTo = local.setListTypeTo;
            $scope.listFilter = $scope.spaceStatusList[0];
            $scope.gridTypeList = ['4', '6', '12'];
            $scope.gridType = 0;
            $scope.assignmentIndex = -1;
            $scope.reservationIndex = -1;
            $scope.eliminationIndex = -1;
            $scope.resetIndex = -1;
            $scope.emailForAssignment = [];
            $scope.passwordForAssignment = [];
            $scope.emailForReservation = [];
            $scope.emailForReset = [];

            $scope.cuzType = local.cuzType;
            $scope.getPanelClass = local.getPanelClass;
            $scope.getColorClass = local.getColorClass;

            $scope.viewCustomerFiles = local.viewCustomerFiles;
            $scope.removeCustomerSpace = local.removeCustomerSpace;
            $scope.resetCustomerSpace = local.resetCustomerSpace;
            $scope.reserveCustomerSpace = local.reserveCustomerSpace;
            $scope.assignCustomerSpace = local.assignCustomerSpace;

            $scope.assignmentOk = local.assignmentOk;
            $scope.assignmentCancel = local.assignmentCancel;
            $scope.reservationOk = local.reservationOk;
            $scope.reservationCancel = local.reservationCancel;
            $scope.eliminationOk = local.eliminationOk;
            $scope.eliminationCancel = local.eliminationCancel;
            $scope.resetOk = local.resetOk;
            $scope.resetCancel = local.resetCancel;


            $scope.tempSpaceAssign = {};

            local.init();


        }
    ]);

}(window.angular));