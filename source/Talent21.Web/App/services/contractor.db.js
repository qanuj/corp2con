app.factory('contractorService', ['$http', '$q','db.util', function ($http, $q,util) {
    var v = 'api/v1/';

    var calculatePaging=util.calculatePaging;
    var orderBy = util.orderBy;

    var contractor={};

    contractor.paged = function (page, pageSize) {
        return $http.get(v + 'contractor/paged?$inlinecount=allpages' + calculatePaging(page,pageSize));
    }

    contractor.dashboard = function () {
        return $http.get(v + 'contractor/dashboard');
    }

    contractor.all = function () {
        return $http.get(v + 'contractor/all');
    }

    contractor.get = function (id) {
        return $http.get(v + 'contractor/profile' + (!!id ? '/' + id : ''));
    }

    contractor.update = function (formData) {
        return $http.put(v + 'contractor/profile', formData);
    }

    contractor.search = function (query, page, pageSize) {
        return $http.post(v + 'job/search?$inlinecount=allpages' + calculatePaging(page, pageSize) + orderBy('Id'), query);
    }
    
    contractor.jobById = function (id) {
        return $http.get(v + 'contractor/job/' + id);
    }

    contractor.ApplyToJob = function (id) {
        return $http.post(v + 'contractor/job/' + id + '/apply');
    }

    contractor.applications = function (id) {
        return $http.put(v + '/job/application/' + id + '/apply');
    }
    
    contractor.getJobApplications = function (jobId,page,pageSize) {
        return $http.get(v + 'contractor/job/application?$inlinecount=allpages' + calculatePaging(page, pageSize));
    }

    contractor.favorite = function (id) {
        return $http.put(v + 'contractor/job/application/' + id + '/favorite');
    }

    contractor.getFavoriteById = function (Id) {
        return $http.get(v + 'contractorjob/application/{id}/favorite?$filter=Id eq ' + Id);
    }
   

    contractor.unfavorite = function (id) {
        return $http.delete(v + 'contractor/job/application/' + id + '/favorite');
    }

    contractor.getFavorite = function () {
        return $http.get(v + 'contractor/job/application/favorite/all?type=json');
    }

    contractor.favoriteJob = function (formData) {
        return $http.put(v + 'contractor/job/application/{id}/favorite', formData);
    }

    contractor.deleteFavoriteJob = function (record) {
        return $http.delete(v + 'contractor/job/application/{id}/favorite' + record.id);
    }
    
     contractor.pagedSchedule = function (page, pageSize) {
       console.log('Page - ', page, ' Pagesize', pageSize);
       return $http.get(v + 'contractor/schedule/paged?$orderby=Id desc' + calculatePaging(page, pageSize));
   }

    contractor.createSchedule = function (formData) {
        return $http.post(v + 'contractor/schedule', formData);
    }

    contractor.editSchedule = function (formData) {
        return $http.put(v + 'contractor/schedule', formData);
    }
    contractor.deleteSchedule = function (record) {
        return $http.delete(v + 'contractor/schedule/' + record.id);
    }

    contractor.getSchedule = function (page,pageSize) {
        return $http.get(v + 'contractor/schedule/all?$orderby=Id desc' + calculatePaging(page,pageSize));
    }

    contractor.getLatestJobs = function (skill, location, page, pageSize) {
        return $http.get(v + 'contractor/latest/jobs/' + skill + '/' + location + '?$orderby=Id desc' + calculatePaging(page, pageSize));
    }

    contractor.topEmployers = function (skill, location, page, pageSize) {
        return $http.get(v + 'contractor/top/employers/' + skill + '/' + location + '?' + calculatePaging(page, pageSize));
    }

    return contractor;
}]);