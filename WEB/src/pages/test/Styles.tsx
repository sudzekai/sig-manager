import "./stylesPage.css";

export default function StylesPage() {

    return (
        <>
            <div className="flex flex-wrap gap-2">
                <div className="flex flex-col">
                    <label className=" font-semibold text-xl mb-2">
                        Кнопки
                    </label>

                    <div className="flex flex-wrap gap-2">
                        <div className="body">
                            btn
                            <div className="body-content">
                                <button className="btn">
                                    enabled
                                </button>
                                <button className="btn" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>

                        <div className="body">
                            btn-outline
                            <div className="body-content">
                                <button className="btn-outline">
                                    enabled
                                </button>
                                <button className="btn-outline" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>

                        <div className="body">
                            btn-primary
                            <div className="body-content">
                                <button className="btn btn-primary">
                                    enabled
                                </button>
                                <button className="btn btn-primary" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>

                        <div className="body">
                            btn-primary-outline
                            <div className="flex flex-wrap gap-1">
                                <button className="btn btn-primary-outline">
                                    enabled
                                </button>
                                <button className="btn btn-primary-outline" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>

                        <div className="body">
                            btn-secondary
                            <div className="body-content">
                                <button className="btn btn-secondary">
                                    enabled
                                </button>
                                <button className="btn btn-secondary" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>

                        <div className="body">
                            btn-secondary-outline
                            <div className="flex flex-wrap gap-1">
                                <button className="btn btn-secondary-outline">
                                    enabled
                                </button>
                                <button className="btn btn-secondary-outline" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>

                        <div className="body">
                            btn-danger
                            <div className="flex flex-wrap gap-1">
                                <button className="btn btn-danger">
                                    enabled
                                </button>
                                <button className="btn btn-danger" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>

                        <div className="body">
                            btn-danger-outline
                            <div className="flex flex-wrap gap-1">
                                <button className="btn btn-danger-outline">
                                    enabled
                                </button>
                                <button className="btn btn-danger-outline" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>

                        <div className="body">
                            btn-success
                            <div className="flex flex-wrap gap-1">
                                <button className="btn btn-success">
                                    enabled
                                </button>
                                <button className="btn btn-success" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>

                        <div className="body">
                            btn-success-outline
                            <div className="flex flex-wrap gap-1">
                                <button className="btn btn-success-outline">
                                    enabled
                                </button>
                                <button className="btn btn-success-outline" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>

                        <div className="body">
                            btn-info
                            <div className="flex flex-wrap gap-1">
                                <button className="btn btn-info">
                                    enabled
                                </button>
                                <button className="btn btn-info" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>

                        <div className="body">
                            btn-info-outline
                            <div className="flex flex-wrap gap-1">
                                <button className="btn btn-info-outline">
                                    enabled
                                </button>
                                <button className="btn btn-info-outline" disabled>
                                    disabled
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}