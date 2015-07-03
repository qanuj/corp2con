app.controller('companySearchController', ['$scope', 'dataService', function ($scope,db) {
    $scope.title = "Contractor : Search Result";
    $scope.query= {
        keywords: '',
        location: '',
        skills:'',
    }
    $scope.search = function (query) {

        db.contractor.paged(query).success(function (result) {
            $scope.records = result;
        });
    }
}]);