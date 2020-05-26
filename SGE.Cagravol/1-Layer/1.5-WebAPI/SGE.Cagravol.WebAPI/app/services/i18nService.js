(function (angular) {
    'use strict';
    var sge = angular.module('sgeApp');

    sge.service('I18nService', [
        function () {
            var buttons = [{
                signUp: 'Alta Expositor',
                login: 'Entrar con mi Usuario',
                send: 'Enviar',
                enter: 'Entrar',
                logOut: 'Salir',
                close: 'Cerrar',
                done: 'Hecho',
                today: 'Hoy',
                clear: 'Limpiar',
                edit: 'Editar',
                delete: 'Eliminar',
                deleteConfirm: 'Confirmar Eliminar',
                remove: 'Borrar',
                activity: 'Actividad',
                ok: 'Aceptar',
                cancel: 'Cancelar',
                viewActivity: 'Ver Actividad',
                download: 'Descargar',
                reserve: 'Reservar',
                reset: 'Resetear',
                assign: 'Asignar',
            }, {}];

            var self = this,                
                local = {                    
                    langListIndex: {
                        es_ES: 0, es: 0,
                        en_EN: 1, en: 1, es_CA: 2, cat: 2,
                        fr_FR: 3, fra: 3,fr: 3,
                        de_DE: 4,de: 4,deu: 4,                        
                        it_IT: 5,it: 5,ita: 5,
                        po_PT: 6,pt: 6,po: 6,por: 6,ptg: 6
                    },
                    errors: [{
                        onDefaultPlatformRequest: "Error al solicitar los parámetros por defecto",
                        onUnknowError: 'No se pudo completar la acción, debido a un error desconocido. Por favor, inténtelo de nuevo más tarde o contacte con el Administrador del sistema. Discule las molestias.',
                        onGettingList: 'Error en la solicitud de Listado. Por favor, contacte con el administrador del sistema.',
                        onReSendFileNoDataFound: 'No se encontraron datos para reenviar el archivo.'
                    }, {}],
                    common: [{
                        appTitle: 'Grafidec',
                        appOwner: 'Grafidec Gestiona, S.L.',
                        appOwnerUrl: 'http://www.grafidec.es',
                        appDescription: '',
                        wellcome: 'Bienvenido',
                        logOut: buttons[0].logOut,
                        login: buttons[0].enter,
                        signUp: buttons[0].signUp,
                        close: buttons[0].close,
                        today: buttons[0].today,
                        clear: buttons[0].clear,
                        done: buttons[0].done,
                        edit: buttons[0].edit,
                        delete: buttons[0].delete,
                        deleteConfirm: buttons[0].deleteConfirm,
                        remove: buttons[0].remove,
                        activity: buttons[0].activity,
                        viewActivity: buttons[0].viewActivity,
                        download: buttons[0].download,
                        ok: buttons[0].ok,
                        cancel: buttons[0].cancel,
                        reserve: buttons[0].reserve,
                        reset: buttons[0].reset,
                        assign: buttons[0].assign,
                        decimalFormatPlease: 'Por favor, use formato numérico con o sin decimales',
                        dateFormat: 'dd/MM/yyyy',
                        dateTimeFormat: 'dd/MM/yyyy HH:mm:ss',
                        filesSent: 'archivos enviados',
                        generalSpace: 'ESPACIO GENERAL',
                        freeSpace: 'Espacio Libre',
                        atDateTime: ' el día/hora ',
                        atTime: ' a las ',
                        atDate: ' el día ',                        
                        errors: {
                            onUnknowError: 'No se pudo completar la acción, debido a un error desconocido. Por favor, inténtelo de nuevo más tarde o contacte con el Administrador del sistema. Discule las molestias.',
                            onGettingList: 'Error en la solicitud de Listado. Por favor, contacte con el administrador del sistema.',                            
                        },
                    }, {}],
                    menus: [{
                        projects: 'Eventos',
                        projectList: 'Lista Eventos',
                        newProject: 'Nuevo Evento',
                        mySpaceActivity: 'Mi Actividad',
                        mySpaceFiles: 'Archivos',
                        mySpaceNewFile: 'Enviar Nuevo Archivo',
                        mySpaceProfile: 'Mi Perfil',
                    }, {}],                    home: [{
                        loginMessage: 'Si ya posee credenciales para entrar al sistema, por favor, use la opciópn "Entrar con mi Usuario".',
                        signUpMessage: 'Si le han proporcionado un Código para darse de Alta en el sistema, use la opción "Alta Expositor"',
                        loginButton: 'Entrar con mi Usuario',
                        signUpButton: 'Alta Expositor'
                    }, {}],
                    login: [{
                        userName: "Email Usuario",
                        password: "Contraseña",
                        passwordConfirmation: "Confirmar Contraseña",
                        notPossibleToLogIn: "No ha sido posible entrar en el sistema. Por favor, contacte con el administrador del sistema."
                    }, {}, {}, {}],
                    signUp: [{
                        title: "Alta Expositor",
                        projectCode: "Código Expositor",
                        instructions: "Para darse de alta como expositor en el sistema, por favor, siga las siguientes instrucciones:",
                        instructions_a: 'Introduzca el "Código de Evento" que le ha sido facilitado. Si no posee este código, solicítelo.',
                        instructions_b: 'Facilite una dirección de correo electrónico como identificador de usuario. Esta dirección de correo electrónico será usada tambien para contactar con usted y/o enviarle notificaciones desde este sistema.',
                        instructions_c: 'Facilite una contraseña de mínimo 6 caracteres. Use números, letras (Mayúsculas y minúsculas), y/o los siguientes símbolos autorizados: <strong style="text-decoration: underline">$ % @ . , - _ ! ¡ ¿ ? # / \ · ( )</u></strong>. Por seguridad, no comparta esta contraseña con nadie, o hágalo sólo si es imprescindible.',
                        instructions_d: 'Una vez haya sido incluido en el sistema, puede entrar al sistema con las credenciales que ha facilitado. Le será enviado un mensaje de correo a la dirección de email que ha facilitado.',
                        registrationFailedBy: 'No fue posible registrar el código, debido al/los siguiente/s motivo/s:',
                        registrationSuccess: 'Acaba de ser registrado en el sistema correctamente. Se le ha enviado un email de confirmación. En 5 segundos será redirigido a la página para entrar al sistema',
                        sendButton: buttons[0].send,
                        field:{
                            signUpCode: 'Codigo Expositor',
                            userName: 'Email Registro',
                            name: 'Nombre Expositor',
                        },
                    }, {}],
                    test: [{
                        testOk: 'El test resultó satisfactorio.',
                        testFail: 'El test ha fallado.'

                    }, {}],
                    panel: [{
                        panelTitle: 'Inicio',
                        panelDescription: 'Este es su panel inicial. Desde aquí puede realizar cualquier acción en el sistema. Por ejemplo, puede comienzar en el menú superior izquierda',

                    }, {}],
                    customerFileList: [{
                        title:"Archivos del Evento"
                    }, {}],
                    managerInfoPanels: [{
                        createProject: {
                            title: 'Crear Nuevo Evento',
                            content: 'Utilice el botón "Nuevo Evento" para crear un nuevo evento.',
                            buttonLabel: 'Nuevo Evento'
                        },
                        manageLastProject: {
                            title: 'Administrar Evento Más Reciente',
                            content: 'Ir a gestionar el evento creado más reciente.',
                            buttonLabel: 'Administrar Evento'
                        },
                        viewActivity: {
                            title: 'Ver Actividad Reciente',
                            content: 'Puede ver la actividad reciente de los clientes, últimos archivos enviados, incidencias, etc.',
                            buttonLabel: 'Ver Más'
                        },
                    }, {}],
                    project: [{
                        createTitle: "Crear Nuevo Evento",
                        editTitle: "Modificar Evento",
                        createDescription: "Añada un nuevo evento para crear espacios donde los expositores puedan enviar archivos de cualquier volumen.",
                        editDescription: "",
                        listTitle: "Listado de Eventos",
                        activityTitle: "Actividad de Expositores",
                        activityDescription: "Aquí puede ver la lista de expositores, junto a un resumen de su actividad. Desde cada uno de ellos puede acceder a su historial de archivos, mensajes y movimientos.",
                        noItemsInList: "No hay eventos activos.",
                        itemDoesNotExist: "El evento no existe.",
                        filter: 'Filtrar...',
                        backToList: 'Ir al Listado',
                        backToActivity: 'Ir a Actividad',
                        doCreate: 'Crear',
                        doSave: 'Guardar',
                        downloadExcel:'Descargar Excel',
                        createdSuccess: 'El Evento ha sido creado con éxito.',
                        modifiedSuccess: 'El Evento ha sido modificado con éxito.',
                        deletedSuccess: 'El Evento ha sido eliminado con éxito.',
                        deleteTitle: 'Eliminar Evento',
                        deleteDescription: 'ATENCIÓN: Si elimina este Evento, tambien eliminará todos los espacios de usuario, archivos subidos e historial de clientes y archivos. ¿Está seguro que desea eliminar este evento?',
                        deleteDescriptionDoesNotExistItem: 'No es posible eliminar un evento que no existe. El identificador del Evento no es válido.',
                        spaceStatus: 'Estado',
                        waitingForCustomerRegistration: '(Esperando Registro)',
                        viewCustomerFiles: 'Ver Archivos',
                        assignSpaceToCustomer: 'Asignar',
                        reserveSpaceForCustomer: 'Reservar',
                        resetSpaceForCustomer: 'Liberar',
                        removeCustomerSpace: 'Eliminar',
                        resetCustomerSpace: 'Liberar Espacio',
                        rightClickForMoreOptionsInfo:'Haga Click con el botón derecho del ratón para más opciones',
                        spaceStatusList: [{ id: 'all', name: 'Todos' }, { id: 'free', name: 'Libres' }, { id: 'ocupied', name: 'Ocupados' }],
                        field: {
                            name: "Nombre",
                            nameInfo: "Nombre que recibe el Evento",
                            description: "Descripción",
                            descriptionInfo: "Detalles del Evento",
                            code: "Código",
                            codeInfo: "Código usado para las altas de expositores. Por ejemplo: EVENTO2016. A cada expositor se le facilitará posteriormente un código compuesto por el Código de Evento, más el Número de Expositor, es decir, si el evento dispone de un total de 50 expositores, los códigos generados irán desde EVENTO2016-1 a EVENTO2016-50",
                            totalStands: "Nº Expositores",
                            startDate: 'Fecha Inicio Evento',
                            finishDate: 'Fecha Fin Evento',
                            extraChargeForSendingDate: 'Fecha Límite Sin Recargo Extra en Envíos',
                            extraChargeForSendingDateInfo: 'Fecha a partir de la cual el usuario deberá pagar un recargo sobre el coste por cada envío',
                            limitForSendingDate: 'Fecha Límite Envíos',
                            limitForSendingDateInfo: 'Fecha a partir de la cual no se podrán realizar más envíos',
                            extraChargePercentage: '% Recargo',
                            notes: 'Observaciones'
                        },
                        errors: {
                            onDelete:'Ocurrió un error al intentar eliminar el evento. Hemos recogido la incidencia automáticamente. Desde SGESoft nos pondremos en contacto con el administrador del sistema.'
                        },
                        validation: {
                            projectName: '',
                            projectCode: '',
                            totalStands: ''
                        }
                    }, {}],
                    myspace: [{
                        mainTitle: "Mi Actividad",
                        mainDescription: "Desde aquí tiene una vista general de todos los archivos que mantiene con su proveedor.",                        
                        listTitle: "Listado de Archivos",
                        noItemsInList: "No hay archivos.",
                        resendFileTitle: 'Re-Enviar Archivo',
                        resendFileDescription:'Puede volver a enviar el mismo archivo, si este ha sido rechazado o verifica que el ya enviado no es correcto. Los dos únicos requisitos es que tenga el mismo nombre de archivo y sean del mismo tipo.',
                        newFileTitle: 'Enviar Archivo',
                        newFileDescription:'Puede enviar cualquier tipo de archivo cuyo tamaño sea inferior o igual a 2 Gigabytes (2.147.483.648 bytes / 2.097.152 Kilobytes / 2048 Megabytes)',
                        itemDoesNotExist: "El archivo no existe.",
                        filter: 'Filtro',
                        fileUploadSuccess:'El Archivo ha sido subido con éxito. Puede comprobarlo en el siguiente enlace: ',
                        backToList: 'Ir al Listado',
                        doAddNewFile: 'Añadir Archivo',
                        doResendFile: 'Reenviar Archivo',
                        doSaveFileData: 'Guardar Datos',
                        new:'Nuevo Envío',
                        edit: 'Modificar Datos Archivo',
                        resend: 'Re-enviar Archivo',
                        history: 'Historia',
                        doSave: 'Guardar',
                        createdSuccess: 'El Archivo ha sido enviado con éxito.',
                        modifiedSuccess: 'El Archivo ha sido re-enviado con éxito.',
                        deletedSuccess: 'El Archivo ha sido eliminado con éxito.',
                        deleteTitle: 'Eliminar Archivo',
                        deleteDescription: 'ATENCIÓN: Si elimina este Archivo, tambien eliminará todos el historial, y no se podrá volver a acceder a él. ¿Está seguro que desea eliminar este archivo?',
                        deleteDescriptionDoesNotExistItem: 'No es posible eliminar un archivo que no existe. El identificador del Archivo no es válido.',
                        remainingSeconds: '%h:%m:%s para completar subida.',
                        fileUploaded:'Subida de archivo completada',
                        removeSelectedFile: 'Quitar Archivo Seleccionado',
                        calculatingRemainingTime: 'Calculando tiempo estimado de subida...',
                        composingFileInDestination: 'Recomponiendo archivo en destino...',                                               
                        field: {
                            wfstate:'Estado',
                            file: 'Archivo',
                            fileInfo: 'Este es el archivo que va a subir a la plataforma',
                            fileNotes: 'Notas Archivo',
                            name: "Nombre",
                            nameInfo: "Nombre Original del Archivo",
                            fileName: "Nombre Original del Archivo",                            
                            description: "Descripción",
                            attach: "Adjuntar Arvhivo",
                            attachInfo: "Seleccione el archivo que desea subir ",
                            descriptionInfo: "Detalles del archivo y su contenido.",
                            notes: 'Observaciones',
                            notesInfo: 'datos añadidos',
                            fileType: 'Tipo de Archivo',
                            fileTypeInfo: 'Seleccione el tipo de archivo que desea subir.',
                            firstDeliveryDate: 'Fecha/Hora envío',
                            version: 'Versión del Archivo',
                            size:'Tamaño'
                        },
                        errors: {
                            onDelete: 'Ocurrió un error al intentar eliminar el Archivo.',
                            onHashUploadChannelEmpty: 'No existe un canal de subida para este archivo.',
                            onUploadErrorOrCancel: 'Ocurrió un error o se canceló la subida de archivo.',
                            onUserCancelUpload: 'El Usuario canceló la subida de archivo.',
                            onResendFileChangeNameTypeDontMatch: 'Para reenviar un archivo, este debe tener el mismo nombre y ser del mismo tipo MIME.'
                        },
                        validation: {
                            projectName: '',
                            projectCode: '',
                            totalStands: ''
                        }
                    }, {}],
                    generalSpace: [{
                        mainTitle: "Espacio General - Archivos",
                        mainDescription: "Desde aquí tiene una vista general de todos los archivos en el Espacio General del Evento, accesible a Organzadores, Supervisores y Managers.",
                        listTitle: "Listado de Archivos",
                        noItemsInList: "No hay archivos.",
                        resendFileTitle: 'Re-Enviar Archivo',
                        resendFileDescription:'Puede volver a enviar el mismo archivo, si este ha sido rechazado o verifica que el ya enviado no es correcto. Los dos únicos requisitos es que tenga el mismo nombre de archivo y sean del mismo tipo.',
                        newFileTitle: 'Enviar Archivo',
                        newFileDescription:'Puede enviar cualquier tipo de archivo cuyo tamaño sea inferior o igual a 2 Gigabytes (2.147.483.648 bytes / 2.097.152 Kilobytes / 2048 Megabytes)',
                        itemDoesNotExist: "El archivo no existe.",
                        filter: 'Filtro',
                        fileUploadSuccess:'El Archivo ha sido subido con éxito. Puede comprobarlo en el siguiente enlace: ',
                        backToList: 'Ir al Listado',
                        doAddNewFile: 'Añadir Archivo',
                        doResendFile: 'Reenviar Archivo',
                        doSaveFileData: 'Guardar Datos',
                        new:'Nuevo Envío',
                        edit: 'Modificar Datos Archivo',
                        resend: 'Re-enviar Archivo',
                        history: 'Historia',
                        doSave: 'Guardar',
                        createdSuccess: 'El Archivo ha sido enviado con éxito.',
                        modifiedSuccess: 'El Archivo ha sido re-enviado con éxito.',
                        deletedSuccess: 'El Archivo ha sido eliminado con éxito.',
                        deleteTitle: 'Eliminar Archivo',
                        deleteDescription: 'ATENCIÓN: Si elimina este Archivo, tambien eliminará todos el historial, y no se podrá volver a acceder a él. ¿Está seguro que desea eliminar este archivo?',
                        deleteDescriptionDoesNotExistItem: 'No es posible eliminar un archivo que no existe. El identificador del Archivo no es válido.',
                        remainingSeconds: '%h:%m:%s para completar subida.',
                        fileUploaded:'Subida de archivo completada',
                        removeSelectedFile: 'Quitar Archivo Seleccionado',
                        calculatingRemainingTime: 'Calculando tiempo estimado de subida...',
                        composingFileInDestination: 'Recomponiendo archivo en destino...',                                               
                        field: {
                            wfstate:'Estado',
                            file: 'Archivo',
                            fileInfo: 'Este es el archivo que va a subir a la plataforma',
                            fileNotes: 'Notas Archivo',
                            name: "Nombre",
                            nameInfo: "Nombre Original del Archivo",
                            fileName: "Nombre Original del Archivo",
                            description: "Descripción",
                            attach: "Adjuntar Archivo",
                            attachInfo: "Seleccione el archivo que desea subir ",
                            descriptionInfo: "Detalles del archivo y su contenido.",
                            notes: 'Observaciones',
                            notesInfo: 'datos añadidos',
                            fileType: 'Tipo de Archivo',
                            fileTypeInfo: 'Seleccione el tipo de archivo que desea subir.',
                            firstDeliveryDate: 'Fecha/Hora envío',
                            version: 'Versión del Archivo',
                            size:'Tamaño'
                        },
                        errors: {
                            onDelete: 'Ocurrió un error al intentar eliminar el Archivo.',
                            onHashUploadChannelEmpty: 'No existe un canal de subida para este archivo.',
                            onUploadErrorOrCancel: 'Ocurrió un error o se canceló la subida de archivo.',
                            onUserCancelUpload: 'El Usuario canceló la subida de archivo.',
                            onResendFileChangeNameTypeDontMatch: 'Para reenviar un archivo, este debe tener el mismo nombre y ser del mismo tipo MIME.'
                        },
                        validation: {
                            projectName: '',
                            projectCode: '',
                            totalStands: ''
                        }
                    }, {}],
                    file: [{
                        historyTitle: 'Historia del Archivo',
                        historyDescription: 'Aquí podrá ver desde el inicio hasta la situación actual toda la actividad alrededor del archivo.',
                        addCommentToState: 'Añadir Comentario',
                        saveNewCommentToState: 'Guardar',
                        cancelNewCommentToState: 'Cancelar',
                        resendFile: 'Enviar Nueva Versión Archivo',
                        wfStateChangedSuccessfully_format: 'Se cambió con éxito el estado del archivo. Ahora el estado es: <strong>{0}</strong>',
                        pleaseAddACommentForStateChange:'Por favor, añada un comentario antes de cambiar el estado del archivo.',
                        wfStates:{
                            FILE_FILED: 'Archivar/Eliminar',
                            FILE_IN_PRODUCTION: 'Pasar a Producción',
                            FILE_IN_REVISION: 'Pasar a Revisión',
                            FILE_IN_UPLOAD: 'Pasar a Archivo En Subida',
                            FILE_RE_UPLOADED: 'Pasar a Archivo en Re-Subida',
                            FILE_LOADED: 'Pasar a Archivo Subido',
                            FILE_READY_FOR_PRODUCTION: 'Pasar a Listo para Producción',
                            FILE_REJECTED: 'Rechazar Archivo',
                            FILE_UPLOAD_FAILED: 'Pasar a Subida Fallida',
                        },
                        field: {
                            newComment: 'Introduzca el Comentario',
                            commentForStateChange: 'Por favor, escriba un comentario que apoye el cambio de estado en el archivo, tal como la causa del cambio, una petición, requerimiento, etc.',
                        },
                        errors: {
                            onFileNotExist: 'No se puede encontrar el archivo',
                            onWFStateChangeFailed: 'Error al cambiar el estado del archivo. Por favor, contacte con su administrador.',
                        },
                    }, {}],
                    services:{
                        file: [{
                            composingFileInDestination: 'Recomponiendo archivo en destino...',
                            errors: {                            
                                onHashUploadChannelEmpty: 'No existe un canal de subida para este archivo.',
                                onUploadErrorOrCancel: 'Ocurrió un error o se canceló la subida de archivo.',
                                onUserCancelUpload: 'El Usuario canceló la subida de archivo.'
                            }
                        }, {}],
                    },                    
                };

            self.langListIndex = local.langListIndex;
            self.file = local.file;
            self.common = local.common;
            self.home = local.home;
            self.login = local.login;
            self.test = local.test;
            self.signUp = local.signUp;
            self.buttons = local.buttons;
            self.panel = local.panel;
            self.customerFileList = local.customerFileList;
            self.managerInfoPanels = local.managerInfoPanels;
            self.menus = local.menus;
            self.project = local.project;
            self.myspace = local.myspace;
            self.generalSpace = local.generalSpace;
            self.gspace = local.generalSpace;
            self.errors = local.errors;
            self.services = local.services;
            return self;
        }
    ]);

}(window.angular));
