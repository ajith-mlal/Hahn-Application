import { RouterConfiguration, Router } from 'aurelia-router';
import { PLATFORM } from 'aurelia-pal';

export class App {

  router: Router;
  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = "User Assets";
    config.map([
      {
        route: 'add-user', name: 'add',
        moduleId: PLATFORM.moduleName('./user-profiles/add-user/add-user'), nav: true, title: 'AddUser', href: '#add-user'
      },
      {
        route: ['', 'list-users'], name: 'list',
        moduleId: PLATFORM.moduleName('./user-profiles/list-users/list-users'), nav: true, title: 'ListUser', href: '#list-users'
      },
      {
        route: ['', 'user-detail/:id'], name: 'detail',
        moduleId: PLATFORM.moduleName('./user-profiles/user-detail/user-detail'), nav: true, title: 'UserDetail', href: '#user-detail'
      },

    ]);
    this.router = router;
  }
}
