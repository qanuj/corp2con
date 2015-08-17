app.controller('contractorSearchController', ['$scope', 'dataService', '$routeParams', function ($scope, db, param) {
    $scope.title = "Jobs : Search Result";

    if(!isNaN(param.idea)){
        param.page = page.idea;
    } else if (param.idea == "match") {
        $scope.searching = "Matching Jobs for you.";
    } else if (param.idea == "month") {
        $scope.searching = "Matching Jobs for you, next month";
    } else if (param.idea == "week") {
        $scope.searching = "Matching Jobs for you, next week";
    }


    $scope.navigate = function (page) {
        $scope.query = {
            keywords: param.q || param.keywords || '',
            location: param.location || '',
            skills:param.skills || ''
        }

        function fetchResults(query, page) {
            db.contractor.search(query, page).success(function (result) {
                $scope.currentPage = page || 1;
                $scope.pages = Math.ceil(result.count / db.pageSize);
                $scope.count = result.count;
                $scope.records = result.items;
                $scope.page = page;
            });
        }

        $scope.search = function (query) {
            var q = '';
            for (var x in query) {
                q += (q === '' ? '?' : '&') + x + '=' + query[x];
            }
            window.location = '#/search' + q;
        }

        fetchResults($scope.query, page || 1);
    }
    $scope.navigate(param.page);
}]);