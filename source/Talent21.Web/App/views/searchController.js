app.controller('searchController', ['$scope', '$location', function ($scope, $location) {
    $scope.search = function (query) {
        window.location='#/search?q=' + query;
        return false;
    }
}]);