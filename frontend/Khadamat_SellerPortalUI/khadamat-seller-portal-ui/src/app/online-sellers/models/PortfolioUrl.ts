import {PortfolioUrlType} from './PortfolioUrlType';

export class PortfolioUrl {
  private _url:string;
  private _type: PortfolioUrlType

  get url(): string {
    return this._url;
  }

  set url(value: string) {
    this._url = value;
  }

  get type(): PortfolioUrlType {
    return this._type;
  }

  set type(value: PortfolioUrlType) {
    this._type = value;
  }

  constructor(url: string, type: PortfolioUrlType) {
    this._url = url;
    this._type = type;
  }
}
