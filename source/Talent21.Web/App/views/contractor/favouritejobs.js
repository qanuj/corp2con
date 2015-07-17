app.controller('companySearchController', ['$scope', 'dataService', function ($scope,db) {
    $scope.title = "Contractor : Search Result";
    $scope.query= {
        keywords: '',
        location: '',
        skills:'',
    }
    db.contractor.paged().success(function (result) {
        $scope.records = result;
    });
}]);