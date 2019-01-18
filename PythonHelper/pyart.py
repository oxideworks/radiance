import matplotlib.pyplot as plt
import fnlib as fn

class pyart:
    dotSize = 10
    lineWidth = 1.4
    lineStyle = '-'
    nextDashes = (None, None)
    nextLabel = None
    textShift = (5, 10)

    def __init__(self):
        fig, ax = plt.subplots(figsize=(10,10), dpi=100)
        plt.style.use('bmh')
        self.fig = fig
        self.ax = ax

    def dots(self, xdata, ydata, color='k', zord=1, label_dots=False, font_size=12):
        self.ax.scatter(xdata, ydata, s=self.dotSize, c=color,
                        zorder=zord, label=self.nextLabel)
        if label_dots:
            for (x,y) in zip(xdata, ydata):
                self.text(x, y, f'({x}; {y})', font_size)
        self.nextLabel = None

    def line(self, xdata, ydata, color='k', zord=1):
        self.ax.plot(xdata, ydata, c=color, zorder=zord,
                     linestyle=self.lineStyle, linewidth=self.lineWidth,
                     dashes=self.nextDashes, label=self.nextLabel)
        self.nextDashes = (None, None)
        self.nextLabel = None

    def line_kx_b(self, k, b, rngx, color='k', zord=1):
        rngy = [k * i + b for i in rngx]
        self.line(rngx, rngy, color=color, zord=zord)

    def set_title(self, title):
        plt.title(title)

    def x_legend(self, label):
        plt.xlabel(label)

    def y_legend(self, label):
        plt.ylabel(label)

    def save(self, name, dpi):
        plt.legend()
        plt.savefig(name, dpi=dpi)

    def show(self):
        plt.legend()
        plt.show()

    def x_deltas(self, xdata, ydata, xdeltas, color='k', zord=1):
        for tripl in zip(xdata, xdeltas, ydata):
            self.ax.plot([tripl[0] - tripl[1], tripl[0] + tripl[1]], [tripl[2], tripl[2]],
                         c=color, zorder=zord, linestyle=self.lineStyle,
                         linewidth=self.lineWidth, dashes=self.nextDashes)
        self.nextDashes = (None, None)

    def y_deltas(self, xdata, ydata, ydeltas, color='k', zord=1):
        for tripl in zip(ydata, ydeltas, xdata):
            self.ax.plot([tripl[2], tripl[2]], [tripl[0] - tripl[1], tripl[0] + tripl[1]],
                         c=color, zorder=zord, linestyle=self.lineStyle,
                         linewidth=self.lineWidth, dashes=self.nextDashes)
        self.nextDashes = (None, None)

    def text(self, x, y, text, fontsize=12):
        (x, y) = fn.zip_with(lambda x, y : x + y, (x, y), self.textShift)
        self.ax.text(x, y, text, fontsize=fontsize)

    def invert_xaxis(self):
        plt.gca().invert_xaxis()

    def invert_yaxis(self):
        plt.gca().invert_yaxis()

    def turn_axis_off(self):
        plt.axis('off')
    
    def turn_axis_on(self):
        plt.axis('on')

    def circle(self, x, y, r, color='k'):
        c = plt.Circle((x, y), r, color=color, fill=False, clip_on=False, linewidth=2)
        self.ax.add_artist(c)