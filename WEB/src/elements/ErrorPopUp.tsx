import { createPortal } from "react-dom";

type ErrorPopUpProps = {
    header: string;
    message: string;
    onExit: Function
};

export default function ErrorPopUp({ header, message, onExit }: ErrorPopUpProps) {
    if (!header)
        header = "Ошибка";

    return createPortal(
        <div className="fixed left-0 top-0 bottom-0 right-0 backdrop-blur-xl z-30">
            <div className="fixed left-1/2 top-1/2 -translate-x-1/2 -translate-y-1/2">
                <div className="frame bg-(--background-primary)/30">
                    <div className="flex flex-col gap-2 w-[300px] p-2">
                        <label className="text-center text-2xl font-semibold text-danger">{header}</label>
                        <label className="text-center">{message}</label>
                        <button className="btn btn-primary mt-2" onClick={() => onExit()}>ОК</button>
                    </div>
                </div>
            </div>
        </div>,
        document.body
    )
}