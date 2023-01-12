export function AutoPopoverLocateShow(direction) {
    const currentClientHeight = window.innerHeight;
    const currentClientWidth = window.innerWidth;

    let popoverBlock = document.querySelector(".popover-block");
    let userDefineBlock = document.querySelector(".user-define-block");
    let popoverBlockStyle = popoverBlock.style;
    popoverBlockStyle.display = "flex";
    popoverBlockStyle.visibility = "hidden";

    if (direction === 0) {
        popoverBlockStyle.right = "100%";
        userDefineBlock.style.flexDirection = "row-reverse";
        let [offsetTop, offsetLeft] = GetOffsetByBody(popoverBlock);
        let offsetRight = currentClientWidth - offsetLeft - popoverBlock.offsetWidth;
        let offsetBottom = currentClientHeight - offsetTop - popoverBlock.offsetHeight;
        if (offsetLeft < 0) {
            if (offsetBottom < 0) {
                if (offsetTop < 0) {
                    if (offsetRight < 0) {
                        return;
                    }
                    else {
                        popoverBlockStyle.left = "100%";
                        popoverBlockStyle.right = "auto";
                        userDefineBlock.style.flexDirection = "row";
                    }
                }
                else {
                    popoverBlockStyle.left = 0;
                    popoverBlockStyle.right = "auto";
                    popoverBlockStyle.bottom = "100%";
                    userDefineBlock.style.flexDirection = "column-reverse";
                }
            }
            else {
                popoverBlockStyle.left = 0;
                popoverBlockStyle.right = "auto";
                popoverBlockStyle.top = "100%";
                userDefineBlock.style.flexDirection = "column";
            }
        }
    }
    if (direction === 1) {
        popoverBlockStyle.left = "100%";
        userDefineBlock.style.flexDirection = "row";
        let [offsetTop, offsetLeft] = GetOffsetByBody(popoverBlock);
        let offsetRight = currentClientWidth - offsetLeft - popoverBlock.offsetWidth;
        let offsetBottom = currentClientHeight - offsetTop - popoverBlock.offsetHeight;

        if (offsetRight < 0) {
            if (offsetBottom < 0) {
                if (offsetTop < 0) {
                    if (offsetLeft < 0) {
                        return;
                    }
                    else {
                        popoverBlockStyle.left = "auto";
                        popoverBlockStyle.right = "100%";
                        userDefineBlock.style.flexDirection = "row-reverse";
                    }
                }
                else {
                    popoverBlockStyle.left = "auto";
                    popoverBlockStyle.right = 0;
                    popoverBlockStyle.bottom = "100%";
                    userDefineBlock.style.flexDirection = "column-reverse";
                }
            }
            else {
                popoverBlockStyle.left = "auto";
                popoverBlockStyle.right = 0;
                popoverBlockStyle.top = "100%";
                userDefineBlock.style.flexDirection = "column";

            }
        }
    }
    if (direction === 2) {
        userDefineBlock.style.flexDirection = "column-reverse";
        let [offsetTop, offsetLeft] = GetOffsetByBody(popoverBlock);
        let offsetRight = currentClientWidth - offsetLeft - popoverBlock.offsetWidth;
        
        if (offsetRight < 0 && offsetLeft > 0) {
            popoverBlockStyle.bottom = "100%";
            popoverBlockStyle.right = 0;
        }
        if (offsetRight > 0 && offsetLeft < 0) {
            popoverBlockStyle.bottom = "100%";
            popoverBlockStyle.left = 0;
        }
        if (offsetRight > 0 && offsetLeft > 0) {
            popoverBlockStyle.bottom = "100%";
        }
    }
    if (direction === 3) {
        userDefineBlock.style.flexDirection = "column";
        let [offsetTop, offsetLeft] = GetOffsetByBody(popoverBlock);
        let offsetRight = currentClientWidth - offsetLeft - popoverBlock.offsetWidth;

        if (offsetRight < 0 && offsetLeft > 0) {
            popoverBlockStyle.top = "100%";
            popoverBlockStyle.right = 0;
        }
        if (offsetRight > 0 && offsetLeft < 0) {
            popoverBlockStyle.top = "100%";
            popoverBlockStyle.left = 0;
        }
        if (offsetRight > 0 && offsetLeft > 0) {
            popoverBlockStyle.top = "100%";
        }
    }
    popoverBlockStyle.visibility = "visible";
}

export function AutoPopoverLocateHidden() {
    let popoverBlock = document.querySelector(".popover-block");
    popoverBlock.style.display = "none";
    popoverBlock.style.left = "auto";
    popoverBlock.style.right = "auto";
    popoverBlock.style.top = "auto";
    popoverBlock.style.bottom = "auto";
}

function GetOffsetByBody(element) {
    let offsetTop = 0;
    let offsetLeft = 0;
    while (element && element.tagName != "body") {
        offsetTop += element.offsetTop;
        offsetLeft += element.offsetLeft;
        element = element.offsetParent;
    }
    
    return [offsetTop, offsetLeft];
}