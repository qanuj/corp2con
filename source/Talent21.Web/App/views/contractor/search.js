app.controller('contractorSearchController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Jobs : Search Result";
    $scope.query= {
        keywords: $routeParams.q,
        location: '',
        skills:''
    }
    function search(query) {
        db.contractor.searchJob(query).success(function (result) {
            $scope.records = result;
        });
    }
    search($scope.query);
}]);