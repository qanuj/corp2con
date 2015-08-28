app.controller('companySearchController', ['$scope', 'dataService', '$routeParams', function ($scope, db, $routeParams) {
    $scope.title = "Contractor : Search Result";
    $scope.save = "Save";

    $scope.navigate = function (page) {
        $scope.query = {
            keywords: $routeParams.q || $routeParams.keywords || '',
            location: $routeParams.location || '',
            folder: $routeParams.folder || '',
            skills: $routeParams.skills || '',
            startrate: $routeParams.startrate || '',
            endrate: $routeParams.endrate || '',
            xfrom: $routeParams.xfrom || '',
            xto: $routeParams.xto || '',
            industry: $routeParams.industry || ''
        }

        function fetchResults(query, page) {
            db.company.search(query, page).success(function (result) {
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
    $scope.navigate($routeParams.page);

    $scope.toggle=function(allSelected) {
        for (var x in $scope.records) {
            $scope.records[x].selected = allSelected;
        }
    }

    db.system.getSkills().success(function (result) {
        $scope.skills = result;
    });

    db.company.getSearchFolders().success(function (result) {
        $scope.folders = result;
    });

    db.system.getLocations().success(function (result) {
        $scope.locations = result;
    });
    db.system.getIndustries().success(function (result) {
        $scope.industries = result;
    });

    $scope.resetFilters = function () {
        $scope.query.keywords = '';
        $scope.query.skills = '';
        $scope.query.location = '';
        $scope.query.startrate = '';
        $scope.query.endrate = '';
        $scope.query.xfrom = '';
        $scope.query.xto = '';
        $scope.query.industry = '';
        $scope.search(query);
    }

    //Slider configs
    $scope.experienceSlider = {
        min: $scope.query.xfrom || 0,
        max: $scope.query.xto || 500,
        ceil: 500,
        floor: 0
    };

    $scope.rateSlider = {
        min: $scope.query.startrate || 0,
        max: $scope.query.endrate || 500,
        ceil: 500,
        floor: 0
    };

    $scope.translate = function (value) {
        return '$' + value;
    }


    $scope.move = function (folder) {
        var i = 0;
        function moveFolder(x, folder, next) {
            if (!$scope.records[x]) {
                $scope.save = "Save";
                return;
            }
            $scope.save = (x + 1);
            if ($scope.records[x].selected == true) {
                db.company.moveContractor($scope.records[x].id, folder).success(next);
            }else next();
        }
        function onNext() {
            moveFolder(++i, folder, onNext);
        }
        moveFolder(i, folder, onNext);
    }

    $scope.$on("slideEnded", function () {
        // user finished sliding a handle 
        console.log('slide ended')
        $scope.query.startrate = $scope.rateSlider.min;
        $scope.query.endrate = $scope.rateSlider.max;
        $scope.query.xfrom = $scope.experienceSlider.min;
        $scope.query.xto = $scope.experienceSlider.max;
        $scope.search($scope.query);

    });


}]);