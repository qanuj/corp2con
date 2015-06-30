app.controller('companySearchController', ['$scope', 'dataService', function ($scope,db) {
	$scope.title = "Contractors : Search Result";
	db.company.paged().success(function (result) {
	    $scope.records = result;
    });
}]);