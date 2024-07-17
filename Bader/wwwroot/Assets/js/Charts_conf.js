let colors = {
    'primary': '#009ef7',
    'success': '#50cd89',
    'info': '#7239ea',
    'warning': '#ffc700',
    'danger': '#f1416c',

    'primary_rgb': '0, 158, 247',
    'success_rgb': '80, 205, 137',
    'info_rgb': '114, 57, 234',
    'warning_rgb': '255, 199, 0',
    'danger_rgb': '241, 65, 108',
    'dark_rgb': '24, 28, 50',

    'white': '#fff',
    'dark': '#2d3044',
    'gray_100': '#f5f8fa',
    'gray_200': '#eff2f5',
    'gray_300': '#E4E6EF',
    'gray_400': '#B5B5C3',
    'gray_500': '#A1A5B7',
    'gray_600': '#7E8299',
    'gray_700': '#5E6278',
    'transparent': 'transparent',
}

let hideGrid = {
    x: {
        grid: {
            display: false
        }
    },
    y: {
        display: false,
        grid: {
            display: false
        }
    }
}

const main_chart = document.getElementById('main_chart').getContext("2d");
const products_chart = document.getElementById('products_chart');
const branches_chart = document.getElementById('branches_chart');
const sales_per_hour_chart = document.getElementById('sales_per_hour_chart');
const return_chart = document.getElementById('return_chart').getContext("2d");
const offer_chart = document.getElementById('offer_chart').getContext("2d");


let gradient_primary = main_chart.createLinearGradient(0, 0, 0, 250);
gradient_primary.addColorStop(0, `rgb(${colors.primary_rgb}, .5)`);
gradient_primary.addColorStop(1, `rgb(${colors.primary_rgb}, .0)`);

let gradient_danger = main_chart.createLinearGradient(0, 0, 0, 250);
gradient_danger.addColorStop(0, `rgb(${colors.danger_rgb}, .5)`);
gradient_danger.addColorStop(1, `rgb(${colors.danger_rgb}, .0)`);

let return_gradient_danger = return_chart.createLinearGradient(0, 0, 0, 200);
return_gradient_danger.addColorStop(0, `rgb(${colors.danger_rgb}, .5)`);
return_gradient_danger.addColorStop(1, `rgb(${colors.danger_rgb}, .0)`);

let offer_gradient_warning = return_chart.createLinearGradient(0, 0, 0, 200);
offer_gradient_warning.addColorStop(0, `rgb(${colors.warning_rgb}, .5)`);
offer_gradient_warning.addColorStop(1, `rgb(${colors.warning_rgb}, .0)`);



let gradient_info = main_chart.createLinearGradient(0, 0, 0, 250);
gradient_info.addColorStop(0, `rgb(${colors.info_rgb}, .5)`);
gradient_info.addColorStop(1, `rgb(${colors.info_rgb}, .0)`);

let gradient_success = main_chart.createLinearGradient(0, 0, 0, 250);
gradient_success.addColorStop(0, `rgb(${colors.success_rgb}, .5)`);
gradient_success.addColorStop(1, `rgb(${colors.success_rgb}, .0)`);

let gradient_gray = return_chart.createLinearGradient(0, 0, 0, 150);
gradient_gray.addColorStop(0, `rgb(${colors.dark_rgb}, .5)`);
gradient_gray.addColorStop(1, `rgb(${colors.dark_rgb}, .0)`);


new Chart(main_chart, {
    type: 'line',
    data: {
        labels: ['٢٨ مايو', '٢٩ مايو', '٣٠ مايو', '٣١ مايو', '١ يونيو', '٢ يونيو', '٣ يونيو'],
        datasets: [{
            label: 'المبيعات',
            data: [21450, 19717, 20555, 18450, 16460, 23750, 19430],
            backgroundColor: gradient_primary,
            borderColor: colors.primary,
        },
        {
            label: 'المدفوعات',
            data: [22450, 20717, 18555, 19450, 18460, 21750, 16430],
            backgroundColor: gradient_danger,
            borderColor: colors.danger,
        }]
    },
    options: {
        Response: true,
        maintainAspectRatio: false,
        scales: hideGrid,
        pointBackgroundColor: colors.white,
        pointBorderWidth: 3,
        pointHitRadius: 15,
        fill: true,
        tension: 0.3,
        plugins: {
            legend: {
                display: true,
                position: 'top',
                align: 'start',
                rtl: true,
                labels: {
                    boxWidth: 15,
                    boxHeight: 15,
                    font: {
                        size: 14,
                        family: "TheSans",
                    }
                }
            },
            tooltip: {
                rtl: true,
                titleFont: {
                    size: 16,
                    family: "TheSans",
                },
                titleAlign: 'right',
                bodyFont: {
                    size: 16,
                    family: "TheSans",
                },
                boxPadding: 5,
            }
        },
    }
});

new Chart(products_chart, {
    type: 'doughnut',
    data: {
        labels: ['كيكة روشية مستطيل وسط', 'كيكة فرنسية مستطيل وسط', 'شوكولاتة', 'دوناتس', 'كعكة'],
        datasets: [{
            label: 'صافي المبيعات',
            data: [12, 19, 3, 5, 3],
            hoverOffset: 8,
            borderWidth: 3,
        }]
    },
    options: {
        Response: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                display: true,
                position: 'left',
                rtl: true,
                // maxWidth: 200,
                labels: {
                    boxWidth: 15,
                    boxHeight: 15,
                    font: {
                        size: 14,
                        family: "TheSans",
                    }
                }
            },
            tooltip: {
                rtl: true,
                titleFont: {
                    size: 16,
                    family: "TheSans",
                },
                titleAlign: 'right',
                bodyFont: {
                    size: 16,
                    family: "TheSans",
                },
                boxPadding: 5,
            }
        },
        scales: {
            x: {
                display: false
            },
            y: {
                display: false
            }
        }
    }
});

new Chart(branches_chart, {
    type: 'doughnut',
    data: {
        labels: ['فرع الشرقية - الجامعة', 'فرع الظهران - الميدان', 'فرع الشرقية - الجامعة', 'فرع الظهران - الميدان', 'فرع الشرقية - الجامعة'],
        datasets: [{
            label: 'صافي المبيعات',
            data: [45, 30, 25, 15, 5],
            hoverOffset: 8,
            borderWidth: 3,
        }]
    },
    options: {
        Response: true,
        maintainAspectRatio: false,
        // aspectRatio: 2,
        plugins: {
            legend: {
                display: true,
                position: 'left',
                rtl: true,
                // maxWidth: 200,
                labels: {
                    boxWidth: 15,
                    boxHeight: 15,
                    font: {
                        size: 14,
                        family: "TheSans",
                    }
                }
            },
            tooltip: {
                rtl: true,
                titleFont: {
                    size: 16,
                    family: "TheSans",
                },
                titleAlign: 'right',
                bodyFont: {
                    size: 16,
                    family: "TheSans",
                },
                boxPadding: 5,
            }
        },
        scales: {
            x: {
                display: false
            },
            y: {
                display: false
            }
        }
    }
});

new Chart(sales_per_hour_chart, {
    type: 'bar',
    data: {
        labels: ['12 AM', '01 AM', '02 AM', '03 AM', '04 AM', '05 AM', '06 AM', '07 AM', '08 AM', '09 AM', '10 AM', '11 AM', '12 PM', '01 PM', '02 PM', '03 PM', '04 PM', '05 PM', '06 PM', '07 PM', '08 PM', '09 PM', '10 PM', '11 PM',],
        datasets: [{
            label: 'المبيعات',
            data: [0, 0, 0, 0, 0, 0, 50, 100, 200, 300, 500, 200, 189, 600, 650, 1070, 2000, 4000, 3000, 2600, 2000, 1500, 1000, 700],
            backgroundColor: `rgb(${colors.primary_rgb}, .8)`,
            borderColor: colors.primary,
        }]
    },
    options: {
        Response: true,
        maintainAspectRatio: false,
        scales: hideGrid,
        pointBackgroundColor: colors.white,
        pointBorderWidth: 3,
        pointHitRadius: 15,
        fill: true,
        tension: 0.3,
        plugins: {
            legend: {
                display: false
            },
            tooltip: {
                rtl: true,
                titleFont: {
                    size: 16,
                    family: "TheSans",
                },
                titleAlign: 'right',
                bodyFont: {
                    size: 16,
                    family: "TheSans",
                },
                boxPadding: 5,
            }
        },
    }
});

new Chart(return_chart, {
    type: 'line',
    data: {
        labels: ['٢٨ مايو', '٢٩ مايو', '٣٠ مايو', '٣١ مايو', '١ يونيو', '٢ يونيو', '٣ يونيو'],
        datasets: [{
            label: 'الإرجاع',
            data: [20555, 19717, 20450, 18450, 17460, 19430, 21450],
            backgroundColor: return_gradient_danger,
            borderColor: colors.danger,
        }]
    },
    options: {
        Response: true,
        maintainAspectRatio: false,
        scales: hideGrid,
        pointBackgroundColor: colors.white,
        pointBorderWidth: 3,
        pointHitRadius: 15,
        fill: true,
        tension: 0.3,
        plugins: {
            legend: {
                display: true,
                position: 'top',
                align: 'start',
                rtl: true,
                labels: {
                    boxWidth: 15,
                    boxHeight: 15,
                    font: {
                        size: 14,
                        family: "TheSans",
                    }
                }
            },
            tooltip: {
                rtl: true,
                titleFont: {
                    size: 16,
                    family: "TheSans",
                },
                titleAlign: 'right',
                bodyFont: {
                    size: 16,
                    family: "TheSans",
                },
                boxPadding: 5,
            }
        },
    }
});

new Chart(offer_chart, {
    type: 'line',
    data: {
        labels: ['٢٨ مايو', '٢٩ مايو', '٣٠ مايو', '٣١ مايو', '١ يونيو', '٢ يونيو', '٣ يونيو'],
        datasets: [{
            label: 'الخصم',
            data: [21450, 19717, 20555, 18450, 16460, 23750, 19430],
            backgroundColor: offer_gradient_warning,
            borderColor: colors.warning,
        }]
    },
    options: {
        Response: true,
        maintainAspectRatio: false,
        scales: hideGrid,
        pointBackgroundColor: colors.white,
        pointBorderWidth: 3,
        pointHitRadius: 15,
        fill: true,
        tension: 0.3,
        plugins: {
            legend: {
                display: true,
                position: 'top',
                align: 'start',
                rtl: true,
                labels: {
                    boxWidth: 15,
                    boxHeight: 15,
                    font: {
                        size: 14,
                        family: "TheSans",
                    }
                }
            },
            tooltip: {
                rtl: true,
                titleFont: {
                    size: 16,
                    family: "TheSans",
                },
                titleAlign: 'right',
                bodyFont: {
                    size: 16,
                    family: "TheSans",
                },
                boxPadding: 5,
            }
        },
    }
});