export class WindowHelper {
  public static scrollToTargetAdjusted(element: any): void {
    // var element = document.getElementById("targetElement");
    const offset: number = 30;
    const bodyRect: any = document.body.getBoundingClientRect().top;
    const elementRect: any = element.getBoundingClientRect().top;
    const elementPosition: any = elementRect - bodyRect;
    const offsetPosition: any = elementPosition - offset;

    window.scrollTo({
      top: offsetPosition,
      behavior: "smooth"
    });
  }
}
