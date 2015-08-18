var app = angular.module('app', [
    // Angular modules 
    'ngAnimate',        // animations
    'ngRoute',          // routing
    'ngSanitize',       // sanitizes html bindings (ex: sidebar.js)

    // 3rd Party Modules
    'ui-rangeSlider',
    'ngTagsInput',
    'angularMoment',
    'ngCkeditor',
    'codemwnci.markdown-edit-preview',
    'humenize',
    'ui.gravatar', //gravtaar for user
    'ui.select', //ui-select for dropdown and multi values.
    'ui.bootstrap',      // ui-bootstrap (ex: carousel, pagination, dialog)
    'blueimp.fileupload', //jQuery File Uploader Component
    'rzModule' //slider module
]).constant('angularMomentConfig', { preprocess: 'utc' });