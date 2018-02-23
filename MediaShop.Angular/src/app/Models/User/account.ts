import {Profile} from './profile';
import {Settings} from './settings';
import {Permissions} from './permissions';

export class Account {
    Login : string;
    Password : string;
    Email : string;
    IsBanned : boolean;
    IsDeleted : boolean;
    Profile : Profile;
    Settings : Settings;
    Permissions : Permissions;
}

