app.controller('companySearchController', ['$scope', 'dataService', function ($scope,db) {
	$scope.title = "Contractors : Search Result";
	db.contractor.paged().success(function (result) {
	    $scope.records = result;
    });
}]);