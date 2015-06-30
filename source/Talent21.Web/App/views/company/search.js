app.controller('companySearchController', ['$scope', 'dataService', function ($scope,db) {
	$scope.title = "Contractor : Search Result";
	db.contractor.paged().success(function (result) {
	    $scope.records = result;
    });
}]);