// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { Injectable } from '@angular/core';
import { Role } from '../models/role.model';
import { SiteEndpoint } from './site-endpoint.service';
import { Site } from '../models/site.model';

export type RolesChangedOperation = 'add' | 'delete' | 'modify';
export interface RolesChangedEventArg { roles: Role[] | string[]; operation: RolesChangedOperation; }

@Injectable()
export class SiteService {
  public static readonly roleAddedOperation: RolesChangedOperation = 'add';
  public static readonly roleDeletedOperation: RolesChangedOperation = 'delete';
  public static readonly roleModifiedOperation: RolesChangedOperation = 'modify';

  constructor(
    private siteEndpoint: SiteEndpoint) {

  }

  getSites() {
    return this.siteEndpoint.getSites<any>();
  }

  updateUser(site: Site) {
    if (!site.id) {
      return this.siteEndpoint.addSite(site);
    } else {
      return this.siteEndpoint.updateSite<Site>(site);
    }
  }

  newSite(site: Site) {
    return this.siteEndpoint.addSite(site);
  }

  deleteSite(id: Number) {
    return this.siteEndpoint.deleteSite(id);
  }
}
