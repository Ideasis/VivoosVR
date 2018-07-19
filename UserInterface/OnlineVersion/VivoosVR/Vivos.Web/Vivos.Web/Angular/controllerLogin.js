(function ($scope) {
    'use strict';

    angular
        .module('app')
        .controller('controllerLogin', controllerLogin);

    controllerLogin.$inject = ['$location', '$scope'];

    function controllerLogin($location, $scope) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'controllerLogin';

        activate();

        function activate() {

        }

        $scope.submit = function () {
            document.getElementById('loginForm').submit();
        };
    }



    
})();
