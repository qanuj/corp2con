app.controller('contractorSearchController', ['$scope', 'dataService', function ($scope, db) {
	$scope.title = "Jobs : Search Result";
	db.contractor.searchJob().success(function (result) {
	    $scope.records = result;
	});
}]);