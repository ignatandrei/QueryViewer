


export class ApplicationDBContext_Client_Table {

  constructor(cc: Partial<ApplicationDBContext_Client_Table> | null = null) {

    if (cc != null) {
      // Object.keys(tilt).forEach((key) => {
      //   (this as any)[key] = (tilt as any)[key];
      // });
      Object.assign(this, cc);
    }
  }
  public idClient : number  = 0;
  public nameClient : string  = '';

}
