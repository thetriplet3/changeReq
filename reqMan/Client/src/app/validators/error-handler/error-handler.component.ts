declare var $: any;
export class ErrorHandler {

    static placements: { [key: string]: object } =
        {
            "br": { from: "bottom", align: "right" }
        }

    public static showErrorMessages(messages: object) {
        for(var message in messages) {
            this.showErrorMessage(messages[message]);
        }
    }
    private static showErrorMessage(message: string): void {
        $.notify({
            icon: "warning",
            message: message
        }, {
                placement: this.placements["br"],
                type: "danger"
            });
    }

}
