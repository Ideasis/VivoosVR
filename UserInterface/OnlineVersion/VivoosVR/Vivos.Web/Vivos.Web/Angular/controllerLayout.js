(function () {
    'use strict';

    angular
        .module('app')
        .controller('controllerLayout', controllerLayout);

    controllerLayout.$inject = ['$scope', '$http', '$window', '$rootScope'];

    function controllerLayout($scope, $http, $window, $rootScope) {
        $scope.title = 'controllerLayout';
        $(".page-loader").show();

        activate();

        function activate() {
            
        }

        $scope.logoff = function logoff() {
            $http.post("/Account/LogOff/").then(function () {
                $window.location.reload();
            });
        };

        $rootScope.$on("show-hard-loading", function () {
            $(".page-loader").show();
        });

        $rootScope.$on("hide-hard-loading", function () {
            $(".page-loader").hide();
        });
    }

    $(document).ready(function () {
        $(".page-loader").hide();
    });
    
    
    
})();
