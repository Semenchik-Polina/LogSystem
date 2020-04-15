export class FilterModel {
  constructor(
    public filterBy: string,
    public userName?: string,
    public type?: number,
    public date?: Date
  ) {}
}
