interface IModalProperties {
  templateName: string;
  model: any;
  title: string;
  onOkModel?: { okFunction?: (any) => any, okText?: string, enableFunction?: any, cssClass?: string };
  onCancelModel?: { onCancelFunction?: (any) => any, isShown?: boolean, cancelText?: string };
  additionalCss?: string;
  onRefreshModel?: any;
  isLarge?: boolean;
  additionalFunctions?: any[];
}

interface KnockoutUtils {
  showModalFromTemplate: (model: IModalProperties) => void;
  showStyledModalFromTemplate: (model: IModalProperties) => void;
}
